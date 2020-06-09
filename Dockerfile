FROM centos
CMD echo Hello World!
CMD sudo yum update -y
CMD sudo rpm -Uvh https://packages.microsoft.com/config/centos/7/packages-microsoft-prod.rpm
CMD sudo yum install dotnet-runtime-3.1
CMD sudo yum install aspnetcore-runtime-3.1