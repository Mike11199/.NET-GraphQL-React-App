import { AppBar, Box, Toolbar, Typography, Button } from '@mui/material'
import { Container } from '@mui/system'
import { Link } from 'react-router-dom'

export default function NavBar(){
    return(

        <AppBar position="static" className="app-bar">
            <Container>
                <Toolbar disableGutters>
                    <Typography
                        variant='h6'
                        noWrap
                        sx={{
                            mr: 12,
                            display:{xs: 'none', md: 'flex'},
                            fontFamily: 'arial',
                            fontWeight: 900,
                            letterSpacing: '.3rem',
                            color:"white",
                            textDecoration: "none",
                            
                        }}
                    >
                        <Link to='/'>Order Management App</Link>
                    </Typography>
                    <Box sx={{flexGrow: 1, display: {xs: 'none', md: 'flex'}}}>
                        <Button
                            key="Customers"
                            sx={{my :2, color: 'white', display: 'block'}}>
                            <Link to='/customers'>Customers</Link>
                        </Button>
                    </Box>                    
                </Toolbar>
            </Container>
        </AppBar>
    )
}