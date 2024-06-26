﻿FROM alpine:latest AS prepare
WORKDIR /app
RUN adduser -u 1000 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:8.0.204-alpine3.19 AS build
RUN apk update && apk upgrade
RUN apk add --no-cache clang build-base zlib-dev
WORKDIR /build
COPY ./my-tiny-proxy.csproj /build/
ARG RUNTIME_ID=linux-musl-x64
RUN dotnet restore -r $RUNTIME_ID
COPY . /build/
RUN dotnet publish \
    -c Release \
    -r $RUNTIME_ID \
    -o /app \
    --no-restore

FROM prepare AS final
COPY --chown=appuser --from=build /app/my-tiny-proxy ./my-tiny-proxy
ENTRYPOINT ["./my-tiny-proxy"]