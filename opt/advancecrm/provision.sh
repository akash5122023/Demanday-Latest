#!/usr/bin/env bash
set -e
PORT="$1"
DB_NAME="$2"
if [[ -z "$PORT" || -z "$DB_NAME" ]]; then
  echo "Usage: $0 <port> <db_name>" >&2
  exit 1
fi
SERVICE_NAME="advancecrm-${PORT}.service"
INSTALL_DIR="/opt/advancecrm"
DOTNET_CMD="/usr/bin/dotnet"
CONNECTION_STRING="Server=localhost;Database=${DB_NAME};Persist Security Info=True;User ID=sa;Password=a3it@.1234;Max Pool Size=50000;Pooling=True;TrustServerCertificate=true;"

# create systemd service file
cat <<SERVICE_EOF >/etc/systemd/system/${SERVICE_NAME}
[Unit]
Description=AdvanceCRM instance on port ${PORT}
After=network.target

[Service]
WorkingDirectory=${INSTALL_DIR}
Environment="ConnectionStrings__Default=${CONNECTION_STRING}"
ExecStart=${DOTNET_CMD} ${INSTALL_DIR}/AdvanceCRM.Web.dll --urls http://0.0.0.0:${PORT}
Restart=always

[Install]
WantedBy=multi-user.target
SERVICE_EOF

# reload systemd and enable service
systemctl daemon-reload
systemctl enable --now ${SERVICE_NAME}
