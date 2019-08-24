import React, { Component, Fragment } from 'react'
import { gql } from 'apollo-boost'
import { Query } from '@apollo/react-components'

const GET_RESERVATIONS_QUERY = gql`
query ReservationsQuery {
    reservations {
        id
        checkinDate
        checkoutDate
        guest {
            id
            name
            registerDate
        }
        room {
            id
            name
            number
            allowedSmoking
            status
        }
    }
}
`

export class Reservations extends Component {
    render() {
        return <Fragment>
            <h2>Reservation List</h2>
            <Query query={GET_RESERVATIONS_QUERY}>
                {
                    ({ loading, error, data }) => {
                        if (loading) return 'Loading...';
                        if (error) return `Error! ${error.message}`;
                        
                        return  <table className="table">
                            <thead className="thead-dark">
                                <tr>
                                    <th scope="col">#</th>
                                    <th scope="col">Room</th>
                                    <th scope="col">Guest</th>
                                    <th scope="col">Checkin Date</th>
                                    <th scope="col">Checkout Date</th>
                                </tr>
                            </thead>
                            <tbody>
                                {data.reservations.map(reservation => (
                                    <tr key={reservation.id}>
                                        <td>{reservation.id}</td>
                                        <td>
                                            <strong>#{reservation.room.number} {reservation.room.name}</strong><br />
                                            <small>Allowed Smoking: {reservation.room.allowedSmoking ? 'yes' : 'no'}</small>
                                        </td>
                                        <td>
                                            <strong>{reservation.guest.name}</strong><br />
                                            <small>Registered Date: {reservation.guest.registerDate}</small>
                                        </td>
                                        <td>{reservation.checkinDate}</td>
                                        <td>{reservation.checkoutDate}</td>    
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    }
                }
            </Query>
        </Fragment>        
    }
}

export default Reservations
