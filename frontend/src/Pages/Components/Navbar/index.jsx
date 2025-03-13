import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import styles from './styles.module.scss'

export default function NavBar(){

    return(
        <Navbar className={styles.navBar}>
            <Container>
                <Nav className={styles.nav2}>
                    <Nav.Link href="/">Clientes</Nav.Link>
                    <Nav.Link href="/produto">Produtos</Nav.Link>
                    <Nav.Link href="/venda">Vendas</Nav.Link>
                </Nav>
            </Container>
        </Navbar>
    )
}
