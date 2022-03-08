import React from "react";
import TicketDropDown from "./TicketDropDown";
import { Form } from "react-bootstrap";
import GAH from "gah-datepicker";

export default class TicketHeader extends React.PureComponent {
  handleInputChange = (e) => {
    this.props.onHeaderChange(e);
  };
  render() {
    return (
      <>
        <Form.Group className="mb-3" controlId="formTicketNumber">
          <Form.Label>شماره</Form.Label>
          <Form.Control
            type="text"
            placeholder="شماره فاکتور"
            name="ticketNumber"
            value={this.props.ticketNumber}
            onChange={this.handleInputChange}
          />
        </Form.Group>
        <Form.Group className="mb-3" controlId="formTicketDate">
          <Form.Label>تاریخ</Form.Label>
          <GAH
            style={{
              width: "100%",
              background: "dodgerblue",
              color: "#fff",
              borderRadius: 6,
            }}
            theme={["dodgerblue", "#fff"]}
            calendarType="jalali"
            value={this.props.ticketDate}
            onChange={(dateObject) => {
              this.handleInputChange({
                target: {
                  name: "ticketDate",
                  value: dateObject.dateString,
                },
              });
            }}
          />
        </Form.Group>
        <Form.Group className="mb-3" controlId="formTicketState">
          <Form.Label>وضعیت</Form.Label>
          <TicketDropDown
            name="ticketState"
            items={this.props.invoiceStateItems}
            selectedItem={this.props.ticketState}
            onChangeSelectedItem={this.handleInputChange}
          />
        </Form.Group>
      </>
    );
  }
}
