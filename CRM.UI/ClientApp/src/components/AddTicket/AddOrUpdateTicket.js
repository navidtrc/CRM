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
      lookups: [],
      ticketNumber: 0,
      ticketDate: this.getCurrentDate(),
      ticketState: {},
      customerFirstName: "",
      customerLastName: "",
      customerPhoneNumber: "",
      devices: [],
    };
  }

  componentDidMount() {
    var myHeaders = new Headers();
    myHeaders.append("Content-Type", "application/json");
    myHeaders.append("Authorization", this.getToken());

    var raw = JSON.stringify({
      Types: ["InvoiceState", "DeviceType", "DeviceBrand"],
    });

    var requestOptions = {
      method: "POST",
      headers: myHeaders,
      body: raw,
      redirect: "follow",
    };

    fetch("api/Lookup/Get", requestOptions)
      .then((response) => response.json())
      .then((resultLookup) => {
        if (this.props.invoiceId !== 0) {
          fetch(`api/Invoice/Get?id=${this.props.invoiceId}`, {
            method: "get",
            headers: {
              Authorization: `Bearer ${this.getToken()}`,
            },
          })
            .then((response) => response.json())
            .then((response) => {
              debugger;
              this.setState({
                lookups: resultLookup.data.data,
                ticketNumber: response.data.data.number,
                ticketDate: response.data.data.datePersian.split(" ")[0],
                customerFirstName: response.data.data.customer.firstName,
                customerLastName: response.data.data.customer.lastName,
                customerPhoneNumber: response.data.data.customer.phoneNumber,
              });
            });
        } else {
          fetch("api/Invoice/GetLastInvoiceNumber", {
            method: "get",
            headers: {
              Authorization: `Bearer ${this.getToken()}`,
            },
          })
            .then((response) => response.json())
            .then((response) => {
              this.setState({
                lookups: resultLookup.data.data,
                ticketNumber: response.data.data,
              });
            });
        }
      })
      .catch((error) => console.log("error", error));
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

  toDropDown(lookup) {
    var result = lookup.map((item) => {
      return {
        id: item.id,
        name: item.aux1,
        title: item.aux2,
      };
    });
    return result;
  }

  render() {
    const invoiceStateItems = this.toDropDown(
      this.state.lookups.filter((item) => item.typeTitle === "InvoiceState")
    );
    const deviceTypeItems = this.toDropDown(
      this.state.lookups.filter((item) => item.typeTitle === "DeviceType")
    );
    const deviceBrandItems = this.toDropDown(
      this.state.lookups.filter((item) => item.typeTitle === "DeviceBrand")
    );
    debugger;
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
                        invoiceStateItems={invoiceStateItems}
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
                        deviceTypeItems={deviceTypeItems}
                        deviceBrandItems={deviceBrandItems}
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
                    type: this.state.lookups.filter(
                      (item) => item.aux2 === device.value.type.title && item.typeTitle === "DeviceType"
                    )[0],
                    brand: this.state.lookups.filter(
                      (item) => item.aux2 === device.value.brand.title && item.typeTitle === "DeviceBrand"
                    )[0],
                    model: device.value.model,
                    accessories: device.value.accessories,
                    description: device.value.description,
                    customerPrice: device.value.customerPrice,
                    shopPrice: device.value.shopPrice,
                    repairWarranty: device.value.repairWarranty,
                    shopWarranty: device.value.shopWarranty,
                    inquiries: device.value.inquiries,
                  };
                });
                debugger;

                const target = {
                  Number: String(this.state.ticketNumber),
                  Date: this.state.ticketDate,
                  State: this.state.ticketState.id,
                  FirstName: this.state.customerFirstName,
                  LastName: this.state.customerLastName,
                  PhoneNumber: this.state.customerPhoneNumber,
                  Devices: devives,
                };
                debugger;
                const token = this.getToken();
                fetch("api/Invoive/Post", {
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
