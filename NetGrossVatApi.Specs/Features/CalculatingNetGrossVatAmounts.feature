Feature: CalculatingNetGrossVatAmounts
	In order to use correctly calculated purchase data
	As an API user
	I want to calculate Net, Gross, VAT amounts for purchases in Austria

@net_gross_vat_calculation
Scenario: Calculating Net, Gross and Vat amounts based on valid input
	Given one of the net, gross or VAT amounts
	And a valid Austrian VAT rate
	When the API is called
	Then the other two missing amounts are calculated and returned