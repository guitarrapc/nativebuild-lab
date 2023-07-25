#!/bin/sh
set -eu

apk --no-cache add make gcc libc-dev
apk --no-cache add python3 perl py3-pip
pip3 install jinja2

cd /src
make clean
make SHARED=1 lib

# generate file test
if ! file "$(readlink -f /src/library/libmbedcrypto.so)" | grep "x86-64"; then
  file "$(readlink -f /src/library/libmbedcrypto.so)"
  echo "file generation arch not desired."
  exit 1
fi
