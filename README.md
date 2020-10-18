# NetGrossVatApi

This api is used to calculate Net, Gross and Vat amounts based on one given amount and Vat rate.

To use this api, use 2 parameters in the query string.

One of the required parameters is "vatRate" and the other is one of the following: "vatAmount", "netAmount" or "grossAmount".



Examples:

/api/vatCalculator?vatRate=0.13&netAmount=5

/api/vatCalculator?vatRate=0.13&grossAmount=5

/api/vatCalculator?vatRate=0.13&vatAmount=5
