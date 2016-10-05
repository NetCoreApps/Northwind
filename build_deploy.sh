#!/bin/bash

# install dependencies
sudo apt-get install jq #install jq for json parsing
pip install --user awscli # install aws cli w/o sudo
export PATH=$PATH:$HOME/.local/bin # put aws in the path

# Set variables
IMAGE_NAME=netcore-northwind
IMAGE_VERSION=latest
ECS_SERVICE=netcoreapps-northwind-service
ECS_TASK=netcoreapps-northwind-task
AWS_DEFAULT_REGION=ap-southeast-2
AWS_ECS_CLUSTER_NAME=default
#AWS_ECS_REPO_DOMAIN={} set in private variable

# Update task definition with env values
sed "s/__ECS_TASK__/$ECS_TASK/g" -i ./task-definition.json
sed "s/__IMAGE_NAME__/$IMAGE_NAME/g" -i ./task-definition.json
sed "s/__AWS_ECS_REPO_DOMAIN__/$AWS_ECS_REPO_DOMAIN/g" -i ./task-definition.json
sed "s/__IMAGE_VERSION__/$IMAGE_VERSION/g" -i ./task-definition.json

# Build/Deploy process
eval $(aws ecr get-login --region $AWS_DEFAULT_REGION) #needs AWS_ACCESS_KEY_ID and AWS_SECRET_ACCESS_KEY envvars
docker build -t $IMAGE_NAME .
docker tag $IMAGE_NAME $AWS_ECS_REPO_DOMAIN/$IMAGE_NAME:$IMAGE_VERSION
docker push $AWS_ECS_REPO_DOMAIN/$IMAGE_NAME:$IMAGE_VERSION
aws ecs register-task-definition --cli-input-json file://task-definition.json --region $AWS_DEFAULT_REGION > /dev/null # Create a new task revision
TASK_REVISION=$(aws ecs describe-task-definition --task-definition $ECS_TASK --region $AWS_DEFAULT_REGION | jq '.taskDefinition.revision') #get latest revision
CREATE_NEW_SERVICE=$(aws ecs list-services --region $AWS_DEFAULT_REGION --cluster $AWS_ECS_CLUSTER_NAME | jq '.serviceArns' | jq 'contains(["arn:aws:ecs:$AWS_DEFAULT_REGION:$AWS_ACCOUNT_NUMBER:service/$ECS_SERVICE"])')
if [ "$CREATE_NEW_SERVICE" = "true" ]; then
    echo "ECS Service already exists"
    aws ecs update-service --cluster $AWS_ECS_CLUSTER_NAME --service $ECS_SERVICE --task-definition "$ECS_TASK:$TASK_REVISION" --desired-count 1 --region $AWS_DEFAULT_REGION > /dev/null #update service with latest task revision
else
    echo "Creating ECS Service $ECS_SERVICE"
    aws ecs create-service --cluster $AWS_ECS_CLUSTER_NAME --service-name $ECS_SERVICE --task-definition "$ECS_TASK:$TASK_REVISION" --desired-count 1 --region $AWS_DEFAULT_REGION > /dev/null #create service
fi
if [ "$(aws ecs list-tasks --service-name $ECS_SERVICE --region $AWS_DEFAULT_REGION | jq '.taskArns' | jq 'length')" -gt "0" ]; then
    TEMP_ARN=$(aws ecs list-tasks --service-name $ECS_SERVICE --region $AWS_DEFAULT_REGION | jq '.taskArns[0]') # Get current running task ARN
    TASK_ARN="${TEMP_ARN%\"}" # strip double quotes
    TASK_ARN="${TASK_ARN#\"}" # strip double quotes
    aws ecs stop-task --task $TASK_ARN --region $AWS_DEFAULT_REGION > /dev/null # Stop current task to force start of new task revision with new image
fi