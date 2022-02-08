import React from "react";
import { Button, Form } from "react-bootstrap";
import TicketDropDown from "./TicketDropDown";
import TicketPrice from "./TicketPrice";
import { Grid, GridColumn } from "@progress/kendo-react-grid";
import "@progress/kendo-theme-default/dist/all.css";
import { DeviceActionCustomCell } from "./DeviceActionCustomCell";
import TicketInquiry from "./TicketInquiry";
const editField = "inEdit";

export default class TicketDevice extends React.Component {
  constructor(props) {
    super(props);

    this.deviceType = [
      { id: "1", name: "shaver", title: "شیور" },
      { id: "2", name: "trimmer", title: "تریمر" },
      { id: "3", name: "epilady", title: "اپیلیدی" },
      { id: "4", name: "hairdryer", title: "سشوار" },
      { id: "5", name: "straighteners", title: "حالت دهنده" },
      { id: "6", name: "toothbrush", title: "مسواک" },
      { id: "7", name: "charger", title: "شارژر" },
      { id: "8", name: "other", title: "غیره" },
    ];

    this.deviceBrand = [
      { id: "1", name: "braun", title: "براون" },
      { id: "2", name: "philips", title: "فیلیپس" },
      { id: "3", name: "remington", title: "رمینگتون" },
      { id: "4", name: "panasonic", title: "پاناسونیک" },
      { id: "5", name: "moser", title: "موزر" },
      { id: "6", name: "wahl", title: "وال" },
      { id: "7", name: "promax", title: "پرو مکس" },
      { id: "8", name: "other", title: "متفرقه" },
    ];

    this.mode = {
      add: { name: "add", title: "ایجاد" },
      update: { name: "update", title: "ویرایش" },
      delete: { name: "delete", title: "حذف" },
      inquiry: { name: "inquiry", title: "" },
    };

    this.state = {
      mode: this.mode.add,
      deviceId: this.newGuid(),
      deviceType: {},
      deviceBrand: {},
      deviceModel: "",
      deviceAccessories: "",
      deviceDescription: "",
      deviceCustomerPrice: 0,
      deviceShopPrice: 0,
      deviceRepairWarranty: false,
      deviceShopWarranty: false,
      deviceTypeErrorMessageShow: false,
      deviceBrandErrorMessageShow: false,
      deviceModelErrorMessageShow: false,
      deviceDescriptionErrorMessageShow: false,
      inquiryModalShow: false,
      inquiryId: 0,
      inquiryReason: "",
      inquiryPrice: 0,
      inquiryCalls: [],
    };
  }

  DeviceEditCell = (props) => (
    <DeviceActionCustomCell
      {...props}
      onEditClick={this.handleUpdateDevice}
      onDeleteClick={this.handleRemoveDevice}
      onInquiryClick={this.handleInquiryDevice}
    />
  );
  handleRemoveDevice = (dataItem) => {
    const target = {
      name: "device",
      action: this.mode.delete,
      value: dataItem.value,
    };
    this.props.onDeviceChange(target);
  };

  handleUpdateDevice = (dataItem) => {
    debugger;
    this.setState({
      mode: this.mode.update,
      deviceId: dataItem.value.Id,
      deviceType: dataItem.value.Type,
      deviceBrand: dataItem.value.Brand,
      deviceModel: dataItem.value.Model,
      deviceAccessories: dataItem.value.Accessories,
      deviceDescription: dataItem.value.Description,
      deviceCustomerPrice: dataItem.value.CustomerPrice,
      deviceShopPrice: dataItem.value.ShopPrice,
      deviceRepairWarranty: dataItem.value.RepairWarranty,
      deviceShopWarranty: dataItem.value.ShopWarranty,
    });
  };

  handleInquiryDevice = (dataItem) => {
    debugger;
    const deviceTemp = this.props.items.filter((device) => {
      debugger;
      return device.value.Id === dataItem.value.Id;
    });
    let id = 0;
    let reason = "";
    let price = 0;
    let calls = [];
    if (deviceTemp[0].value.Inquiries !== undefined) {
      id = deviceTemp[0].value.Inquiries.inquiryId;
      reason = deviceTemp[0].value.Inquiries.inquiryReason;
      price = deviceTemp[0].value.Inquiries.inquiryPrice;
      calls = deviceTemp[0].value.Inquiries.calls;
    }
    this.setState({
      mode: this.mode.inquiry,
      deviceId: dataItem.value.Id,
      inquiryId: id,
      inquiryReason: reason,
      inquiryPrice: price,
      inquiryCalls: calls,
      inquiryModalShow: true,
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

  handleInputChange = (e) => {
    this.setState({ [e.target.name]: e.target.value });
  };

  handleCheckboxChange = (e) => {
    this.setState({ [e.target.name]: e.target.checked });
  };

  handleCustomerPrice = (value) => {
    this.setState({ deviceCustomerPrice: value });
  };

  handleShopPrice = (value) => {
    this.setState({ deviceShopPrice: value });
  };

  checkValidation() {
    let isValid = true;
    if (Object.keys(this.state.deviceType).length === 0) {
      isValid = false;
      this.setState({ deviceTypeErrorMessageShow: true });
    } else {
      this.setState({ deviceTypeErrorMessageShow: false });
    }
    if (Object.keys(this.state.deviceBrand).length === 0) {
      isValid = false;
      this.setState({ deviceBrandErrorMessageShow: true });
    } else {
      this.setState({ deviceBrandErrorMessageShow: false });
    }
    if (!!!this.state.deviceModel) {
      isValid = false;
      this.setState({ deviceModelErrorMessageShow: true });
    } else {
      this.setState({ deviceModelErrorMessageShow: false });
    }
    if (!!!this.state.deviceDescription) {
      isValid = false;
      this.setState({ deviceDescriptionErrorMessageShow: true });
    } else {
      this.setState({ deviceDescriptionErrorMessageShow: false });
    }
    if (!isValid) {
      return false;
    }
    return true;
  }

  clearInputs() {
    this.setState({
      mode: this.mode.add,
      deviceId: this.newGuid(),
      deviceType: {},
      deviceBrand: {},
      deviceModel: "",
      deviceAccessories: "",
      deviceDescription: "",
      deviceCustomerPrice: 0,
      deviceShopPrice: 0,
      deviceRepairWarranty: false,
      deviceShopWarranty: false,
      deviceTypeErrorMessageShow: false,
      deviceBrandErrorMessageShow: false,
      deviceModelErrorMessageShow: false,
      deviceDescriptionErrorMessageShow: false,
    });
  }

  render() {
    return (
      <>
        <div className="container">
          <div className="row align-items-start">
            <div className="col">
              <Form.Group className="mb-3" controlId="formDeviceType">
                <Form.Label>نوع</Form.Label>
                <TicketDropDown
                  name="deviceType"
                  items={this.deviceType}
                  selectedItem={this.state.deviceType}
                  onChangeSelectedItem={this.handleInputChange}
                />
                {this.state.deviceTypeErrorMessageShow && (
                  <Form.Text className="text-danger">
                    انتخاب نوع دستگاه الزامی میباشد
                  </Form.Text>
                )}
              </Form.Group>
            </div>

            <div className="col">
              <Form.Group className="mb-3" controlId="formDeviceBrand">
                <Form.Label>برند</Form.Label>
                <TicketDropDown
                  name="deviceBrand"
                  items={this.deviceBrand}
                  selectedItem={this.state.deviceBrand}
                  onChangeSelectedItem={this.handleInputChange}
                />
                {this.state.deviceBrandErrorMessageShow && (
                  <Form.Text className="text-danger">
                    انتخاب برند دستگاه الزامی میباشد
                  </Form.Text>
                )}
              </Form.Group>
            </div>
            <div className="col">
              <Form.Group className="mb-3" controlId="formDeviceModel">
                <Form.Label>مدل</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="مدل"
                  value={this.state.deviceModel}
                  onChange={this.handleInputChange}
                  name="deviceModel"
                />
                {this.state.deviceModelErrorMessageShow && (
                  <Form.Text className="text-danger">
                    انتخاب مدل دستگاه الزامی میباشد
                  </Form.Text>
                )}
              </Form.Group>
            </div>
          </div>
          <div className="row align-items-center">
            <div className="col">
              <Form.Group className="mb-3" controlId="formDeviceAccessories">
                <Form.Label>متعلقات</Form.Label>
                <Form.Control
                  as="textarea"
                  rows={3}
                  value={this.state.deviceAccessories}
                  onChange={this.handleInputChange}
                  name="deviceAccessories"
                />
              </Form.Group>
            </div>
            <div className="col">
              <Form.Group className="mb-3" controlId="formDeviceDescription">
                <Form.Label>توضیحات</Form.Label>
                <Form.Control
                  as="textarea"
                  rows={3}
                  value={this.state.deviceDescription}
                  onChange={this.handleInputChange}
                  name="deviceDescription"
                />
                {this.state.deviceDescriptionErrorMessageShow && (
                  <Form.Text className="text-danger">
                    توضیحات الزامی میباشد
                  </Form.Text>
                )}
              </Form.Group>
            </div>
          </div>
          <div className="row align-items-end">
            <div className="col">
              <Form.Group className="mb-3" controlId="formDeviceCustomerPrice">
                <Form.Label>قیمت مشتری</Form.Label>
                <TicketPrice
                  value={this.state.deviceCustomerPrice}
                  inputId="deviceCustomerPrice"
                  onInputChange={this.handleCustomerPrice}
                />
              </Form.Group>
            </div>
            <div className="col">
              <Form.Group className="mb-3" controlId="formDeviceShopPrice">
                <Form.Label>قیمت فروشگاه</Form.Label>
                <TicketPrice
                  value={this.state.deviceShopPrice}
                  inputId="deviceShopPrice"
                  onInputChange={this.handleShopPrice}
                />
              </Form.Group>
            </div>
          </div>
          <div className="row align-items-end">
            <div className="col">
              <Form.Group className="mb-3" controlId="formDeviceRepairWarranty">
                <Form.Check
                  type="switch"
                  id="formDeviceRepairWarranty-switch"
                  label="گارانتی تعمیر"
                  checked={this.state.deviceRepairWarranty}
                  onChange={this.handleCheckboxChange}
                  name="deviceRepairWarranty"
                />
              </Form.Group>
            </div>
            <div className="col">
              <Form.Group className="mb-3" controlId="formDeviceShopWarranty">
                <Form.Check
                  type="switch"
                  id="formDeviceShopWarranty-switch"
                  label="گارانتی شرکتی"
                  checked={this.state.deviceShopWarranty}
                  onChange={this.handleCheckboxChange}
                  name="deviceShopWarranty"
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
            const target = {
              name: "device",
              action: this.state.mode,
              value: {
                Id: this.state.deviceId,
                Type: this.state.deviceType,
                Brand: this.state.deviceBrand,
                Model: this.state.deviceModel,
                Accessories: this.state.deviceAccessories,
                Description: this.state.deviceDescription,
                CustomerPrice: this.state.deviceCustomerPrice,
                ShopPrice: this.state.deviceShopPrice,
                RepairWarranty: this.state.deviceRepairWarranty,
                ShopWarranty: this.state.deviceShopWarranty,
              },
            };
            this.props.onDeviceChange(target);
            this.clearInputs();
          }}
        >
          {this.state.mode === this.mode.add
            ? this.mode.add.title
            : this.mode.update.title}
        </Button>
        <div dir="rtl" className="k-rtl">
          <Grid
            style={{
              marginTop: "10px",
            }}
            data={this.props.items}
            editField={editField}
          >
            <GridColumn field="value.Type.title" title="نوع" />
            <GridColumn field="value.Brand.title" title="برند" />
            <GridColumn field="value.Model" title="مدل" />
            <GridColumn field="value.ShopPrice" title="مبلغ فروشگاه" />
            <GridColumn field="value.CustomerPrice" title="مبلغ مشتری" />
            <GridColumn cell={this.DeviceEditCell} width="220px" />
          </Grid>
          {this.state.inquiryModalShow && (
            <TicketInquiry
              onModalClose={() => {
                this.setState({ inquiryModalShow: false });
              }}
              show={this.state.inquiryModalShow}
              deviceId={this.state.deviceId}
              inquiryId={this.state.inquiryId}
              reason={this.state.inquiryReason}
              price={this.state.inquiryPrice}
              calls={this.state.inquiryCalls}
              onSubmit={(inquiry) => {
                this.props.onChangeInquiry(inquiry);
                this.clearInputs();
              }}
            />
          )}
        </div>
      </>
    );
  }
}
