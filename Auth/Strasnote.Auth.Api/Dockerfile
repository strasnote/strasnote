FROM --platform=$BUILDPLATFORM golang:alpine AS build

ARG TARGETPLATFORM
ARG BUILDPLATFORM

RUN echo "Build: $BUILDPLATFORM, target: $TARGETPLATFORM" > /log

FROM bcgdesign/aspnet:net5-2.0.7
COPY --from=build /home/runner/work/strasnote/strasnote/publish /app/live

ENV ASPNET_ASSEMBLY=Strasnote.Auth.Api.dll
