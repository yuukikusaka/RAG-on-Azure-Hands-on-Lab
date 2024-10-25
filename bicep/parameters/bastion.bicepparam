using '../templates/bastion.bicep'

// Bastion を展開する仮想ネットワーク (Hub)
param vnetName = 'vnet-hub-mcwfy25q2g1'
param addressPrefix = '10.0.0.0/16'

// サブネット
param subnetPrefix = '10.0.10.0/26'

// リソースを展開する際に使用するネットワークの名前
param spokeNetworkName = 'vnet-mcwfy25q2g1'

// Bastion Host の名前
param bastionName = 'bas-mcwfy25q2g1'

param location = 'westus'
