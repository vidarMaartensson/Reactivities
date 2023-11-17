import { Container, Header } from "semantic-ui-react";
import { Link } from "react-router-dom";


export default function HomePage () {
    return (
        <Container style={{marginTop: '7em'}}>
            <Header>Home page!</Header>
            <h3>Go to <Link to='/activities'>Activities</Link></h3>
        </Container>
    )
}