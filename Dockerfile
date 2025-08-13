# Используем SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Копируем csproj и восстанавливаем зависимости
COPY ["TicTacToe.Api.csproj", "./"]
RUN dotnet restore "TicTacToe.Api.csproj"

# Копируем весь проект и билдим
COPY . .
RUN dotnet publish "TicTacToe.Api.csproj" -c Release -o /app/publish

# Финальный образ
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TicTacToe.Api.dll"]
