FROM centos
CMD echo Hello World!
CMD sudo yum update -y
RUN apt-get install apt-transport-https
RUN apt-get update
RUN apt-get install dotnet-sdk-3.1
RUN dotnet
