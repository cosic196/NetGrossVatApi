# NetGrossVatAPINetGrossVatAPI

This is a simple API which calculates net, gross and vat amounts of a purchase based on two input parameters:
1. One of: netAmount, grossAmount, vatAmount
2. vatRate


## Constraints

The client&#39;s needs were to make the API return an error with a meaningful message in case of:
- missing or invalid (0 or non-numeric) amount input
- **more than one amount input**
- missing or invalid (0 or non-numeric) amount input
- if multiple errors occur in a single call, try to return as many as possible

As you can see by the second constraint, the API had **interdependent input parameters** (netAmount, grossAmount and vatAmount).
To achieve this, OnlyOneOfRequiredAttribute was implemented which can be used on an input model with the names of interdependent parameters given as input.
To see the use case look at Amount.cs


## Use of BDD and TDD

Some specs were given in Gherkin which were used to form specs with **Specflow**.

.NET&#39;s **TestServer** class was used to simulate HttpRequests in-memory to test the behaviour of the API.

For testing the actual business logic, which is located in the .NET class library project called Logic, simple unit tests (**MSTest**) were written.


## OpenAPI and SwaggerUI

OpenAPI documentation and SwaggerUI were implemented using **Swashbuckle**. 

Since OpenAPI doesn&#39;t support interdependent input parameters, amount parameters have comments which describe their interdependency.

SwaggerUI page is located in the /index.html uri.


## Result and Evelope classes

The Result and Envelope classes are inspired by **Vladimir Khorikov&#39;s Pluralsight course :
"Applying Functional Principles in C#"**

I highly recommend this course for any C# developer :exclamation:


## How to run

API can simply be deployed using the provided **docker-compose.yml**


## How to use

To get a more detailed view on the use cases and return values, look at the OpenAPI specs or run the SwaggerUI page.
Here are some examples of valid requests (all of the requests are GET requests):

http://localhost:5000/api/vatcalculator?grossAmount=12&vatRate=0.1

http://localhost:5000/api/vatcalculator?netAmount=8&vatRate=0.2

http://localhost:5000/api/vatcalculator?vatAmount=10&vatRate=0.5