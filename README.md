# React AG-Grid 

- Added ag-grid alpine style which automatically includes filtering and sorting capabilities.  Set up by passing columnDefs into agGrid based on a typescript interface for Customers from the GraphQL Schema.
  
![image](https://github.com/Mike11199/.NET-GraphQL-TypeScript-App/assets/91037796/9186647a-9106-452a-8df7-3a37a2ea3660)




# Frontend - TypeScript

- Type "yarn start" to run.
- Using Vite as much faster than create-react-app (written in Go with hot module replacement as oppossed to webpack), and yarn (faster than npm).  Using typescript to mirror internship tech stack.

<img src="https://github.com/Mike11199/.NET-GraphQL-React-App/assets/91037796/02f1ba06-b6cf-4d8a-8a20-af80e9f7ce42" alt="image" style="width: 50%; height: auto;">

<br/>
<br/>
<br/>

- Added graphql codegen to generate code from graphql schema using "graphql-codegen init" to generate schema.ts file in frontend/graphql/generated folder.
  - https://the-guild.dev/graphql/codegen/plugins/typescript/typescript-react-apollo#usage-example

<br/>
 
```js

export function useGetCustomersQuery(baseOptions?: Apollo.QueryHookOptions<GetCustomersQuery, GetCustomersQueryVariables>) {
        const options = {...defaultOptions, ...baseOptions}
        return Apollo.useQuery<GetCustomersQuery, GetCustomersQueryVariables>(GetCustomersDocument, options);
      }
export function useGetCustomersLazyQuery(baseOptions?: Apollo.LazyQueryHookOptions<GetCustomersQuery, GetCustomersQueryVariables>) {
          const options = {...defaultOptions, ...baseOptions}
          return Apollo.useLazyQuery<GetCustomersQuery, GetCustomersQueryVariables>(GetCustomersDocument, options);
        }
export type GetCustomersQueryHookResult = ReturnType<typeof useGetCustomersQuery>;
export type GetCustomersLazyQueryHookResult = ReturnType<typeof useGetCustomersLazyQuery>;
export type GetCustomersQueryResult = Apollo.QueryResult<GetCustomersQuery, GetCustomersQueryVariables>;

```
<br/>
<br/>
<br/>


- Used codegen generated GraphQL query to call function to retrieve data from C# backend (running on localhost for now)

```js

import React from 'react';
import { useGetCustomersQuery } from '../../../graphql/generated/schema';

export default function CustomersDashboard() {

    const { data:customersData, loading, error } = useGetCustomersQuery();

    if (loading) {
        return <div>Loading...</div>
    }

    if (error || !customersData){
        return <div>Error...</div>
    }
    return (
        <div>
            <h2>Customers</h2>
            <ul>
                {customersData.customers?.map( customer=> (
                    <li key={customer?.id}>{customer?.firstName}</li>
                ))}
            </ul>
        </div>
    )
}


```

  
<br/>
<br/>
<br/>

```graphql
query getCustomers {
  customers {
    id
    firstName
    lastName
    contactNumber
    address {
      addressLine1      
      addressLine2
      city
      state
      country
      isDeleted
    }
    orders{
      id
      orderDate
  }
  }
}
```    

<br/>
<br/>
<br/>

# Backend - .NET-GraphQL-React-App

- Set up the GraphQL API using C# classes, entities (Address, Customer, Orders) and dependency injection of OMAContext.
- Added a C# interface, which is an abstract class that can contain properties and methods, but not fields/variables.
- Added GraphQL Voyager to allow one to visualize the GraphQL API as an interactive graph via the "/graphql-voyager" endopoint.
- Can view schema and run queries in Banana Cake Pop, which is a GraphQL IDE when running in local host with 'dotnet run' in backend folder

![image](https://github.com/Mike11199/.NET-GraphQL-React-App/assets/91037796/2cf96c89-271b-4677-84ae-06975092bdff)

<br/>
<br/>

![image](https://github.com/Mike11199/.NET-GraphQL-React-App/assets/91037796/06c323e3-2c90-42f9-8d5f-79c1cb8c7b77)

<br/>
<br/>

![image](https://github.com/Mike11199/.NET-GraphQL-React-App/assets/91037796/ac719aef-3c07-4ed2-ba0d-c792cffd8c91)


<br/>
<br/>
<br/>

# #Backend -C# Query and Service

-Query in C# that implements GraphQL 

```cs
using Core.Entities;
using Core.Interfaces;

namespace API.GraphQL
{
    public class Query
    {

        [UseFiltering]  // can use one endpoint and pass filters to it with this
        public IQueryable<Customer> GetCustomers([Service] ICustomerService customerService )  
        {
            return customerService.GetCustomersAndOrders(); 
        }

        [UseFiltering]
        public IQueryable<Order> GetOrders([Service] IOrderService orderService)  
        {
            return orderService.GetOrders();
        }

    }
}

```

<br/>
<br/>

-Service in C# that is used by GraphQL Query

```cs
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    { 

        private readonly IDbContextFactory<OMAContext> _contextFactory;

        public OrderService(IDbContextFactory<OMAContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IQueryable<Order> GetOrders()
        {
            var context = _contextFactory.CreateDbContext();
            context.Database.EnsureCreated();

            return context.Orders
                .Where(o => !o.IsDeleted)
                .Include(o => o.Customer);
        }
    }
}
```



