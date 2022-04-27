# Marketplace Services - CSI 5112 #Group number 13
Backend services for Marketplace project - CSI 5112 Software Engineering at uOttawa

## Setup:

1. Download and install .NET SDK: https://dotnet.microsoft.com/en-us/download
2. Install VSCode or Visual Studio from: https://visualstudio.microsoft.com/downloads/
3. Open `marketplace_services_CSI5112.csproj` in VSCode or Visual Studio 
4. Build and Run project: (Ctrl + F5 or CMD + F5)
5. Navigate to `localhost:<PORT>/swagger` on your browser to view all available services

## Deployment:

1. Dockerhub: https://hub.docker.com/repository/docker/vasu5235/marketplace_services13
2. ECS public endpoint: https://services.vlearnings.net/api/
3. AWS_REGION: us-east-1
4. ECR_REPOSITORY: 620174838346.dkr.ecr.us-east-1.amazonaws.com
5. ECS_SERVICE: app-marketplace-service
6. ECS_CLUSTER: app-mp-cluster

## Running locally through docker (currently preferred):

1. Install docker on your machine: https://docs.docker.com/get-docker/
2. Run from terminal: `docker run --pull=always -p 80:80 vasu5235/marketplace_services13`

## Testing

1. Download postman collection json files from `/QA` folder 
2. Import the collection json files in Postman on your local machine
3. Configure URL parameter - Right click on imported collection and click on **Variables**
4. Set variable name to **url** and initial & current value as the API endpoints e.g. https://localhost:7136/api or http://3.93.177.49/api

## NOTE: Please pull docker image on your local machine to test our APIs or use our EC2 instance endpoint: http://3.93.177.49/api.

## Documentation

1. Inside `/documentation` folder, you can find our documentation for Data Model and API Collection

## Continous Deployment

1. Deployments are triggered for every new release and can be run manually (if user has permissions) from the `Actions` tab
2. `docker-release.yml` - Builds and uploads a new docker image of the services
3. `ecs-release.yml` - Deploys docker public image to ECR and deploys to ECS and utilizes two loadbalancers.
