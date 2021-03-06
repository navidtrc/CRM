import React from "react";
import { Button, Form } from "react-bootstrap";
import TicketDropDown from "./TicketDropDown";
import TicketPrice from "./TicketPrice";
import TicketInquiry from "./TicketInquiry";
import DeviceTable from "./DeviceTable";

export default class TicketDevice extends React.Component {
  constructor(props) {
    super(props);

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

  handleRemoveDevice = (id) => {
    debugger;
    if (this.state.mode === this.mode.update) {
      alert("در حالت ویرایش امکان حذف وجود ندارد.");
      return;
    }
    const item = this.props.items.filter(
      (item) => item.value.id === id
    )[0];
    const target = {
      name: "device",
      action: this.mode.delete,
      value: item.value,
    };
    this.props.onDeviceChange(target);
  };

  handleUpdateDevice = (id) => {
    debugger;
    const item = this.props.items.filter(
      (item) => item.value.id === id
    )[0];
    this.setState({
      mode: this.mode.update,
      deviceId: item.value.id,
      deviceType: item.value.type,
      deviceBrand: item.value.brand,
      deviceModel: item.value.model,
      deviceAccessories: item.value.accessories,
      deviceDescription: item.value.description,
      deviceCustomerPrice: item.value.customerPrice,
      deviceShopPrice: item.value.shopPrice,
      deviceRepairWarranty: item.value.repairWarranty,
      deviceShopWarranty: item.value.shopWarranty,
    });
  };

  handleInquiryDevice = (dataItem) => {
    const item = this.props.items.filter(
      (item) => item.value.id === dataItem
    )[0];
    const deviceTemp = this.props.items.filter((device) => {
      debugger;
      return device.value.id === item.value.id;
    });
    let id = 0;
    let reason = "";
    let price = 0;
    let calls = [];
    if (deviceTemp[0].value.inquiries !== undefined) {
      id = deviceTemp[0].value.inquiries.inquiryId;
      reason = deviceTemp[0].value.inquiries.inquiryReason;
      price = deviceTemp[0].value.inquiries.inquiryPrice;
      calls = deviceTemp[0].value.inquiries.calls;
    }
    this.setState({
      mode: this.mode.inquiry,
      deviceId: item.value.id,
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
    const gridItems = this.props.items.map((item) => {
      debugger;
      return {
        id: item.value.id,
        type: item.value.type.title,
        brand: item.value.brand.title,
        model: item.value.model,
      };
    });
    return (
      <>
        <div className="container">
          <div className="row align-items-start">
            <div className="col">
              <Form.Group className="mb-3" controlId="formDeviceType">
                <Form.Label>نوع</Form.Label>
                <TicketDropDown
                  name="deviceType"
                  items={this.props.deviceTypeItems}
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
                  items={this.props.deviceBrandItems}
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
                id: this.state.deviceId,
                type: this.state.deviceType,
                brand: this.state.deviceBrand,
                model: this.state.deviceModel,
                accessories: this.state.deviceAccessories,
                description: this.state.deviceDescription,
                customerPrice: this.state.deviceCustomerPrice,
                shopPrice: this.state.deviceShopPrice,
                repairWarranty: this.state.deviceRepairWarranty,
                shopWarranty: this.state.deviceShopWarranty,
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
        <div style={{ height: 300, width: "100%" }}>
          <DeviceTable
            items={gridItems}
            onRemoveItem={this.handleRemoveDevice}
            onUpdateItem={this.handleUpdateDevice}
            onInquiryItem={this.handleInquiryDevice}
          />
        </div>

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
      </>
    );
  }
}
