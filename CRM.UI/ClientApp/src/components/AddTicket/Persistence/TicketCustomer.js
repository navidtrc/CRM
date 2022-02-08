import React from "react";
import { Form } from "react-bootstrap";

export default class TicketCustomer extends React.Component {

  handleInputChange = (e) => {
    this.props.onCustomerChange(e)
  };

  render() {
    return (
      <>
        <Form.Group className="mb-3" controlId="formCustomerFirstName">
          <Form.Label>نام</Form.Label>
          <Form.Control
            type="text"
            placeholder="نام"
            value={this.props.customerFirstName}
            name="customerFirstName"
            onChange={this.handleInputChange}
          />
        </Form.Group>
        <Form.Group className="mb-3" controlId="formCustomerLastName">
          <Form.Label>نام خانوادگی</Form.Label>
          <Form.Control
            type="text"
            placeholder="نام خانوادگی"
            value={this.props.customerLastName}
            name="customerLastName"
            onChange={this.handleInputChange}
          />
        </Form.Group>
        <Form.Group className="mb-3" controlId="formCustomerPhoneNumber">
          <Form.Label>شماره تماس</Form.Label>
          <Form.Control
            type="text"
            placeholder="شماره تماس"
            value={this.props.customerPhoneNumber}
            name="customerPhoneNumber"
            onChange={this.handleInputChange}
          />
        </Form.Group>
      </>
    );
  }
}
