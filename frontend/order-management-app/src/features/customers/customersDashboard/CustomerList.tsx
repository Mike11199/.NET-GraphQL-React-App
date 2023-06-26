import { Address, Customer } from "../../../graphql/generated/schema"
import { useState, useMemo } from "react"
import { AgGridReact } from "ag-grid-react";
import { ColDef } from "ag-grid-community";
import 'ag-grid-community/styles/ag-grid.css'
import 'ag-grid-community/styles/ag-theme-alpine.css'
import 'ag-grid-community/styles/ag-theme-balham.css'


interface CustomerListProps {
    customers: Customer[]
}

export default function CustomerList({customers}: CustomerListProps) {
    const [columnDefs] = useState([
        {
            field: 'id',
            width: 50,
            suppressSizeToFit: true
        },        
        {field: 'firstName'},
        {field: 'lastName'},        
        {field: 'contactNumber'},        
        {field: 'email'},        
        {
            field: 'address',
            cellRenderer: function(params: any) {
                const address = params.value as Address
                return address.addressLine1
                    + ', ' + address.addressLine2
                    + ', ' + address.city
                    + ', ' + address.state
                    + ', ' + address.country
            }    
        }       
    ])

    // By wrapping the computation in useMemo, the value of defaultColDef will only be calculated once when the component is initially rendered,
    // and subsequent re-renders will use the memoized value unless the dependencies of useMemo change.
    const defaultColDef = useMemo(() => ({
        sortable: true,
        filter: true,
        resizable: true,
    }), [])

    return (
        <div className="ag-theme-balham-dark" style={{ height: 500, width: "100%" }}>
        <AgGridReact
          rowData={customers}
          columnDefs={columnDefs as ColDef[]}
          defaultColDef={defaultColDef}
        />
      </div>
    )

}