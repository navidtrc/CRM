import React from "react";
import TicketCustomer from "./Persistence/TicketCustomer";
import TicketHeader from "./Persistence/TicketHeader";
import TicketDevice from "./Persistence/TicketDevice";
import { Modal, Tab, Row, Col, Nav } from "react-bootstrap";
import authService from "../api-authorization/AuthorizeService";

export default class AddOrUpdateTicket extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      ticketNumber: 0,
      ticketDate: this.getCurrentDate(),
      ticketState: {},
      customerFirstName: "",
      customerLastName: "",
      customerPhoneNumber: "",
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
          return f.value.id === device.value.id;
        });
        copyDevices[indexUpdate].value = device.value;
        this.setState({ devices: copyDevices });
        return true;
      case "delete":
        const afterDelete = this.state.devices.filter(function (item) {
          return item.value.id !== device.value.id;
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
      return device.value.id === inquiry.value.deviceId;
    });

    selectedDevice[0].value.inquiries = inquiry.value;

    const target = {
      name: "device",
      action: { name: "update" },
      value: selectedDevice[0].value,
    };

    this.handleDeviceChange(target);
  };

  async getToken() {
    const token = await authService.getAccessToken();
    return token;
  }

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
                const devives = this.state.devices.map((device) => {
                  return {
                    state: device.action.name,
                    type: device.value.type.title,
                    brand: device.value.brand.title,
                    model: device.value.model,
                    accessories: device.value.accessories,
                    description: device.value.description,
                    customerPrice: device.value.customerPrice,
                    shopPrice: device.value.shopPrice,
                    repairWarranty: device.value.repairWarranty,
                    shopWarranty: device.value.shopWarranty,
                    // inquiries: device.value.inquiries,
                  };
                });

                const target = {
                  number: this.state.ticketNumber,
                  date: this.state.ticketDate,
                  state: this.state.ticketState.id,
                  firstName: this.state.customerFirstName,
                  lastName: this.state.customerLastName,
                  phoneNumber: this.state.customerPhoneNumber,
                  devices: devives,
                };
                debugger;
                const token = this.getToken();
                fetch("api/ticket", {
                  method: "post",
                  headers: {
                    "Content-Type": "application/json",
                    Authorization: `Bearer ${token}`,
                  },
                  body: JSON.stringify(target),
                });
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
