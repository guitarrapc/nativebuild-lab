# Dockerfile
# First image to build the binary
FROM alpine as builder

RUN apk --no-cache add make gcc libc-dev
COPY . /src
RUN mkdir /pkg && cd /src && make && make DESTDIR=/pkg install
