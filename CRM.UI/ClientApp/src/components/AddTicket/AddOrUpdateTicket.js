import React from "react";
import TicketCustomer from "./Persistence/TicketCustomer";
import TicketHeader from "./Persistence/TicketHeader";
import TicketDevice from "./Persistence/TicketDevice";
import { Modal, Tab, Row, Col, Nav } from "react-bootstrap";

export default class AddOrUpdateTicket extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      ticketNumber: 0,
      ticketDate: this.getCurrentDate(),
      ticketState: {},
      customerFirstName: "",
      customerLastName: "",
      customerPhoneName: "",
      devices: [],
    };
  }
  getCurrentDate = () => {
    let currentDate = new Date()
      .toLocaleDateString("fa-IR")
      .replace(/([۰-۹])/g, (token) =>
        String.fromCharCode(token.charCodeAt(0) - 1728)
      );
    return currentDate;
  };
  closeModal = () => {
    this.props.onModalClose();
  };
  handleHeaderChange = (e) => {
    this.setState({ [e.target.name]: e.target.value });
  };
  handleCustomerChange = (e) => {
    this.setState({ [e.target.name]: e.target.value });
  };
  handleDeviceChange = (device) => {
    let copyDevices = JSON.parse(JSON.stringify(this.state.devices));

    switch (device.action.name) {
      case "add":
        copyDevices.push(device);
        this.setState({ devices: copyDevices });
        return true;
      case "update":
        const indexUpdate = copyDevices.findIndex((f) => {
          debugger;
          return f.value.Id === device.value.Id;
        });
        copyDevices[indexUpdate].value = device.value;
        this.setState({ devices: copyDevices });
        return true;
      case "delete":
        const afterDelete = this.state.devices.filter(function (item) {
          return item.value.Id !== device.value.Id;
        });
        this.setState({ devices: afterDelete });
        return true;
      default:
        console.log("ERROR");
        return false;
    }
  };
  handleInquiryChange = (inquiry) => {
    debugger;

    let selectedDevice = this.state.devices.filter((device) => {
      debugger;
      return device.value.Id === inquiry.value.deviceId;
    });

    selectedDevice[0].value.Inquiries = inquiry.value;

    const target = {
      name: "device",
      action: { name: "update" },
      value: selectedDevice[0].value,
    };

    this.handleDeviceChange(target);
  };

  render() {
    console.log(this.state.devices);
    return (
      <>
        <Modal
          show={this.props.show}
          size="xl"
          className="rtl"
          centered
          dialogClassName="modal-90w"
        >
          <Modal.Header>
            <Modal.Title>ثبت تیکت جدید</Modal.Title>
          </Modal.Header>
          <Modal.Body>
            <Tab.Container id="left-tabs-example" defaultActiveKey="first">
              <Row>
                <Col sm={3}>
                  <Nav variant="pills" className="flex-column">
                    <Nav.Item>
                      <Nav.Link eventKey="first">تیکت</Nav.Link>
                    </Nav.Item>
                    <Nav.Item>
                      <Nav.Link eventKey="second">مشتری</Nav.Link>
                    </Nav.Item>
                    <Nav.Item>
                      <Nav.Link eventKey="third">اجناس</Nav.Link>
                    </Nav.Item>
                    {/* <Nav.Item>
                      <Nav.Link eventKey="fourth">مالی</Nav.Link>
                    </Nav.Item> */}
                  </Nav>
                </Col>
                <Col sm={9}>
                  <Tab.Content>
                    <Tab.Pane eventKey="first">
                      <TicketHeader
                        ticketNumber={this.state.ticketNumber}
                        ticketDate={this.state.ticketDate}
                        ticketState={this.state.ticketState}
                        onHeaderChange={this.handleHeaderChange}
                      />
                    </Tab.Pane>
                    <Tab.Pane eventKey="second">
                      <TicketCustomer
                        customerFirstName={this.state.customerFirstName}
                        customerLastName={this.state.customerLastName}
                        customerPhoneNumber={this.state.customerPhoneNumber}
                        onCustomerChange={this.handleCustomerChange}
                      />
                    </Tab.Pane>
                    <Tab.Pane eventKey="third">
                      <TicketDevice
                        items={this.state.devices}
                        onDeviceChange={this.handleDeviceChange}
                        onChangeInquiry={this.handleInquiryChange}
                      />
                    </Tab.Pane>
                    {/* <Tab.Pane eventKey="fourth">
                      <h1>tab4</h1>
                    </Tab.Pane> */}
                  </Tab.Content>
                </Col>
              </Row>
            </Tab.Container>
          </Modal.Body>
          <Modal.Footer>
            <button
              variant="success"
              className="btn btn-success"
              onClick={() => {
                const target = {
                  Number: this.state.ticketNumber,
                  Date: this.state.ticketDate,
                  State: this.state.ticketState,
                  FirstName: this.state.customerFirstName,
                  LastName: this.state.customerLastName,
                  PhoneNumber: this.state.customerPhoneNumber,
                  Devices: this.state.devices,
                };
                fetch("/", {
                  method: "post",
                  headers: { "Content-Type": "application/json" },
                  body: target,
                });
                console.log(target);
                this.closeModal();
              }}
            >
              ثبت
            </button>
            <button
              variant="danger"
              className="btn btn-danger"
              onClick={this.closeModal}
            >
              انصراف
            </button>
          </Modal.Footer>
        </Modal>
      </>
    );
  }
}
