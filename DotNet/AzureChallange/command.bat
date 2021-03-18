$RESOURCEGROUP=learn-432c55a5-ac67-4fa6-a2d8-2b7358f757c4
$STORAGEACCT=learnstorage$(openssl rand -hex 5)
$FUNCTIONAPP=learnfunctions$(openssl rand -hex 5)

az storage account create \
  --resource-group "$RESOURCEGROUP" \
  --name "$STORAGEACCT" \
  --kind StorageV2 \
  --location centralus

az functionapp create \
  --resource-group "$RESOURCEGROUP" \
  --name "$FUNCTIONAPP" \
  --storage-account "$STORAGEACCT" \
  --runtime node \
  --consumption-plan-location centralus \
  --functions-version 2