import React from "react";
import { Button, Col, Form, Modal, Nav, Row, Tab } from "react-bootstrap";
import TicketPrice from "./TicketPrice";
import GAH from "gah-datepicker";
import { Grid, GridColumn } from "@progress/kendo-react-grid";
import { InquiryCallActionCustomCell } from "./InquiryCallActionCustomCell";
const editField = "inEdit";

export default class TicketInquiry extends React.Component {
  constructor(props) {
    super(props);
    this.mode = {
      add: { name: "add", title: "ایجاد" },
      update: { name: "update", title: "ویرایش" },
      delete: { name: "delete", title: "حذف" },
    };
    debugger;

    this.state = {
      modeCall: this.mode.add,
      deviceId: this.props.deviceId,
      inquiryId: this.props.inquiryId,
      inquiryReason: this.props.reason,
      inquiryPrice: this.props.price,
      inquiryCallId: this.newGuid(),
      inquiryCallReason: "",
      inquiryCallDate: this.getCurrentDate(),
      inquiryCallTime: "",
      inquiryCallDateTime: "",
      inquiryCallAnswer: false,
      inquiryCallReasonErrorMessageShow: false,
      inquiryCallDateErrorMessageShow: false,
      inquiryCallTimeErrorMessageShow: false,
      calls: this.props.calls,
    };
  }
  handleInquiryCallChange = (call) => {
    let copyCalls = JSON.parse(JSON.stringify(this.state.calls));
    switch (call.action.name) {
      case "add":
        copyCalls.push(call);
        this.setState({ calls: copyCalls });
        return true;
      case "update":
        const indexUpdate = copyCalls.findIndex((f) => {
          return f.value.Id === call.value.Id;
        });
        copyCalls[indexUpdate].value = call.value;
        this.setState({ calls: copyCalls });
        return true;
      case "delete":
        const afterDelete = this.state.calls.filter(function (item) {
          return item.value.Id !== call.value.Id;
        });
        this.setState({ calls: afterDelete });
        return true;
      default:
        console.log("ERROR");
        return false;
    }
  };
  checkValidation() {
    let isValid = true;

    if (!!!this.state.inquiryCallReason) {
      isValid = false;
      this.setState({ inquiryCallReasonErrorMessageShow: true });
    } else {
      this.setState({ inquiryCallReasonErrorMessageShow: false });
    }
    if (!!!this.state.inquiryCallDate) {
      isValid = false;
      this.setState({ inquiryCallDateErrorMessageShow: true });
    } else {
      this.setState({ inquiryCallDateErrorMessageShow: false });
    }
    if (!!!this.state.inquiryCallTime) {
      isValid = false;
      this.setState({ inquiryCallTimeErrorMessageShow: true });
    } else {
      this.setState({ inquiryCallTimeErrorMessageShow: false });
    }
    if (!isValid) {
      return false;
    }
    return true;
  }
  InquiryEditCell = (props) => (
    <InquiryCallActionCustomCell
      {...props}
      onEditClick={this.handleUpdateCall}
      onDeleteClick={this.handleRemoveCall}
    />
  );
  handleRemoveCall = (dataItem) => {
    const target = {
      name: "inquiryCall",
      action: this.mode.delete,
      value: dataItem.value,
    };
    this.handleInquiryCallChange(target);
  };

  handleUpdateCall = (dataItem) => {
    this.setState({
      modeCall: this.mode.update,
      inquiryCallId: dataItem.value.Id,
      inquiryCallReason: dataItem.value.CallReason,
      inquiryCallDate: dataItem.value.CallDate,
      inquiryCallTime: dataItem.value.CallTime,
      inquiryCallDateTime: dataItem.value.CallDateTime,
      inquiryCallAnswer: dataItem.value.CallAnswer,
    });
  };
  newGuid = () => {
    return ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, (c) =>
      (
        c ^
        (crypto.getRandomValues(new Uint8Array(1))[0] & (15 >> (c / 4)))
      ).toString(16)
    );
  };
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
  handleInputChange = (e) => {
    this.setState({ [e.target.name]: e.target.value });
  };
  handleCheckboxChange = (e) => {
    this.setState({ [e.target.name]: e.target.checked });
  };
  handlePrice = (value) => {
    this.setState({ inquiryPrice: value });
  };
  clearInputs() {
    this.setState({
      modeCall: this.mode.add,
      inquiryCallId: this.newGuid(),
      inquiryCallReason: "",
      inquiryCallDate: this.getCurrentDate(),
      inquiryCallTime: "",
      inquiryCallDateTime: "",
      inquiryCallAnswer: false,
      inquiryCallReasonErrorMessageShow: false,
      inquiryCallDateErrorMessageShow: false,
      inquiryCallTimeErrorMessageShow: false,
    });
  }

  render() {
    return (
      <Modal show={this.props.show} size="lg" className="rtl" centered>
        <Modal.Header>
          <Modal.Title>استعلام</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Tab.Container id="left-tabs-example" defaultActiveKey="first">
            <Row>
              <Col sm={3}>
                <Nav variant="pills" className="flex-column">
                  <Nav.Item>
                    <Nav.Link eventKey="first">اظهارات</Nav.Link>
                  </Nav.Item>
                  <Nav.Item>
                    <Nav.Link eventKey="second">تماس ها</Nav.Link>
                  </Nav.Item>
                </Nav>
              </Col>
              <Col sm={9}>
                <Tab.Content>
                  <Tab.Pane eventKey="first">
                    <div className="container">
                      <div className="row align-items-start">
                        <div className="col">
                          <Form.Group
                            className="mb-3"
                            controlId="formInquiryReason"
                          >
                            <Form.Label>توضیحات مربوطه</Form.Label>
                            <Form.Control
                              as="textarea"
                              rows={5}
                              value={this.state.inquiryReason}
                              onChange={this.handleInputChange}
                              name="inquiryReason"
                            />
                          </Form.Group>
                        </div>
                      </div>
                      <div className="row align-items-start">
                        <div className="col">
                          <Form.Group
                            className="mb-3"
                            controlId="formInquiryPrice"
                          >
                            <Form.Label>برآورد هزینه</Form.Label>
                            <TicketPrice
                              value={this.state.inquiryPrice}
                              inputId="inquiryPrice"
                              onInputChange={this.handlePrice}
                            />
                          </Form.Group>
                        </div>
                      </div>
                    </div>
                  </Tab.Pane>
                  <Tab.Pane eventKey="second">
                    <div className="container">
                      <div className="row align-items-start">
                        <div className="col">
                          <Form.Group
                            className="mb-3"
                            controlId="formInquiryCallReason"
                          >
                            <Form.Label>توضیحات مکالمه</Form.Label>
                            <Form.Control
                              as="textarea"
                              rows={5}
                              value={this.state.inquiryCallReason}
                              onChange={this.handleInputChange}
                              name="inquiryCallReason"
                            />
                            {this.state.inquiryCallReasonErrorMessageShow && (
                              <Form.Text className="text-danger">
                                توضیحات مربوطه الزامی میباشد
                              </Form.Text>
                            )}
                          </Form.Group>
                        </div>
                      </div>
                      <div className="row align-items-start">
                        <div className="col">
                          <Form.Group
                            className="mb-3"
                            controlId="formInquiryCallDate"
                          >
                            <Form.Label>تاریخ تماس</Form.Label>
                            <GAH
                              style={{
                                width: "100%",
                                background: "dodgerblue",
                                color: "#fff",
                                borderRadius: 6,
                              }}
                              value={this.state.inquiryCallDate}
                              theme={["dodgerblue", "#fff"]}
                              calendarType="jalali"
                              onChange={(dateObject) => {
                                this.setState({
                                  inquiryCallDate: dateObject.dateString,
                                });
                              }}
                            />
                            {this.state.inquiryCallDateErrorMessageShow && (
                              <Form.Text className="text-danger">
                                تاریخ تماس الزامی میباشد
                              </Form.Text>
                            )}
                          </Form.Group>
                        </div>
                        <div className="col">
                          <Form.Group
                            className="mb-3"
                            controlId="formInquiryCallTime"
                          >
                            <Form.Label>ساعت تماس</Form.Label>
                            <Form.Control
                              type="time"
                              placeholder="ساعت"
                              name="inquiryCallTime"
                              value={this.state.inquiryCallTime}
                              onChange={this.handleInputChange}
                            />
                            {this.state.inquiryCallTimeErrorMessageShow && (
                              <Form.Text className="text-danger">
                                ساعت تماس الزامی میباشد
                              </Form.Text>
                            )}
                          </Form.Group>
                        </div>

                        <div className="col">
                          <Form.Group
                            className="mb-3"
                            controlId="formInquiryAnswer"
                          >
                            <Form.Check
                              type="switch"
                              id="formInquiryAnswer-switch"
                              label="پاسخ؟"
                              checked={this.state.inquiryCallAnswer}
                              onChange={this.handleCheckboxChange}
                              name="inquiryCallAnswer"
                            />
                          </Form.Group>
                        </div>
                      </div>
                    </div>
                    <Button
                      variant="info"
                      className="btn btn-info"
                      onClick={() => {
                        if (!this.checkValidation()) {
                          return;
                        }
                        debugger;
                        const target = {
                          name: "inquiryCall",
                          action: this.state.modeCall,
                          value: {
                            Id: this.state.inquiryCallId,
                            CallReason: this.state.inquiryCallReason,
                            CallDate: this.state.inquiryCallDate,
                            CallTime: this.state.inquiryCallTime,
                            CallDateTime: `${this.state.inquiryCallDate} ${this.state.inquiryCallTime}`,
                            CallAnswer: this.state.inquiryCallAnswer,
                          },
                        };
                        this.clearInputs();
                        this.handleInquiryCallChange(target);
                      }}
                    >
                      {this.state.modeCall === this.mode.add
                        ? this.mode.add.title
                        : this.mode.update.title}
                    </Button>
                    <div dir="rtl" className="k-rtl">
                      <Grid
                        style={{
                          marginTop: "10px",
                        }}
                        data={this.state.calls}
                        editField={editField}
                      >
                        <GridColumn field="value.CallDateTime" title="زمان" />
                        <GridColumn cell={this.InquiryEditCell} width="200px" />
                      </Grid>
                      <TicketInquiry
                        onModalClose={() => {
                          this.setState({ inquiryModalShow: false });
                        }}
                        show={this.state.inquiryModalShow}
                      />
                    </div>
                  </Tab.Pane>
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
              const inquiry = {
                name: "inquiry",
                value: {
                  deviceId: this.props.deviceId,
                  inquiryId: this.state.inquiryId,
                  inquiryReason: this.state.inquiryReason,
                  inquiryPrice: this.state.inquiryPrice,
                  calls: this.state.calls,
                },
              };
              this.props.onSubmit(inquiry);
              this.props.onModalClose();
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
    );
  }
}
