FROM centos
CMD echo Hello World!
RUN sudo apt-get update
RUN apt-get install apt-transport-https
RUN apt-get update
RUN apt-get install dotnet-sdk-3.1
RUN dotnet
