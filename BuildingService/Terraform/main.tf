terraform {
  required_version = ">= 0.11" 
 backend "azurerm" {
  storage_account_name = "__terraformstorageaccount__"
    container_name       = "terraform"
    key                  = "terraform.tfstate"
	access_key  ="__storagekey__"
  features{}
	}
	}
  provider "azurerm" {
    version = "=2.0.0"
features {}
}
resource "azurerm_resource_group" "dev" {
  name     = "faso-web-rg"
  location = "eastus"
}

resource "azurerm_app_service_plan" "dev" {
  name                = "__appserviceplan__"
  location            = "${azurerm_resource_group.dev.location}"
  resource_group_name = "${azurerm_resource_group.dev.name}"

  sku {
    tier = "Free"
    size = "F1"
  }
}

resource "azurerm_app_service" "dev" {
  name                = "__appservicename__"
  location            = "${azurerm_resource_group.dev.location}"
  resource_group_name = "${azurerm_resource_group.dev.name}"
  app_service_plan_id = "${azurerm_app_service_plan.dev.id}"

}


resource "azurerm_sql_server" "dev" {
  name                         = "${var.prefix}-03sqlsvr"
  resource_group_name          = "${azurerm_resource_group.dev.name}"
  location                     = "${azurerm_resource_group.dev.location}"
  version                      = "12.0"
  administrator_login          = var.sqladmin
  administrator_login_password = var.sqlpassword
}

resource "azurerm_sql_database" "fcs-tf" {
  name                             = "${var.prefix}-db"
  resource_group_name              = "${azurerm_resource_group.dev.name}"
  location                         = "${azurerm_resource_group.dev.location}"
  server_name                      = "${azurerm_sql_server.dev.name}"
  edition                          = "Basic"
  collation                        = "SQL_Latin1_General_CP1_CI_AS"
  create_mode                      = "Default"
  requested_service_objective_name = "Basic"
}

# Enables the "Allow Access to Azure services" box as described in the API docs
# https://docs.microsoft.com/en-us/rest/api/sql/firewallrules/createorupdate

resource "azurerm_sql_firewall_rule" "dev" {
  name                = "allow-azure-services"
  resource_group_name = "${azurerm_resource_group.dev.name}"
  server_name         = "${azurerm_sql_server.dev.name}"
  start_ip_address    = "0.0.0.0"
  end_ip_address      = "0.0.0.0"
}
