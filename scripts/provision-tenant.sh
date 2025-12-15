#!/usr/bin/env bash
# Map a tenant subdomain to a local application port in Nginx
# Usage: provision-tenant.sh <subdomain> <port> [map_file]
set -euo pipefail

if [ "$#" -lt 2 ]; then
  echo "Usage: $0 <subdomain> <port> [map_file]" >&2
  exit 1
fi

SUBDOMAIN=$1
PORT=$2
MAP_FILE=${3:-/etc/nginx/conf.d/tenants.map}

mkdir -p "$(dirname "$MAP_FILE")"
# Remove existing mapping for the subdomain if present
if [ -f "$MAP_FILE" ]; then
  sed -i.bak "/^$SUBDOMAIN\.example\.com /d" "$MAP_FILE"
fi

echo "$SUBDOMAIN.example.com $PORT;" >> "$MAP_FILE"

nginx -s reload

echo "Mapped $SUBDOMAIN.example.com to $PORT and reloaded Nginx"
