import React from "react";
import TicketDropDown from "./TicketDropDown";
import { Form } from "react-bootstrap";
import GAH from "gah-datepicker";

export default class TicketHeader extends React.PureComponent {
  constructor(props) {
    super(props);
    this.ticketStateItems = [
      { id: "1", name: "NotSentYet", title: "ارسال نشده" },
      { id: "2", name: "SentToRepair", title: "ارسال شده برای تعمیرگاه" },
      { id: "3", name: "BackFromRepair", title: "برگشت از تعمیرگاه" },
      { id: "4", name: "NeedInquiry", title: "نیاز به استعلام" },
      { id: "5", name: "Repairing", title: "در حال تعمیر" },
      { id: "6", name: "Ready", title: "آماده" },
      { id: "7", name: "Done", title: "تحویل داده شده" },
    ];
  }
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
            items={this.ticketStateItems}
            selectedItem={this.props.ticketState}
            onChangeSelectedItem={this.handleInputChange}
          />
        </Form.Group>
      </>
    );
  }
}
