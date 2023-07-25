#!/bin/sh
set -eu

apt-get update
apt-get install -yq --no-install-suggests --no-install-recommends make gcc libc-dev sudo
cd /src
make clean
make arminstall
make aarch64build

# generate file test
if ! file "$(readlink -f /src/lib/libzstd.so)" | grep "ARM aarch64"; then
  file "$(readlink -f /src/lib/libzstd.so)"
  echo "file generation arch not desired."
  exit 1
fi
