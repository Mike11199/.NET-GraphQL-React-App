import './styles.css'
import { ApolloClient, ApolloProvider, InMemoryCache } from '@apollo/client'
import CustomersDashboard from '../../features/customers/customersDashboard/CustomersDashboard'
import { BrowserRouter, Routes, Route } from 'react-router-dom'
import Layout from './Layout'
import HomePage from '../../features/home/HomePage'

const client = new ApolloClient({
  cache: new InMemoryCache({
    typePolicies: {}
  }),
  uri: import.meta.env.VITE_API_SCHEMA_URL
})

function App() {
  

  return (
    <>
      <ApolloProvider client={client}>
        <BrowserRouter>
          <Routes>
            <Route path="/" element={<Layout />}> 
              <Route index element ={<HomePage/>}></Route>
              <Route path="customers"  element ={<CustomersDashboard />}></Route>
            </Route>
          </Routes>          
        </BrowserRouter>        
      </ApolloProvider>
    </>
  )
}

export default App
