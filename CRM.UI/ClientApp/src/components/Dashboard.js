import React, { Component } from "react";
import authService from "./api-authorization/AuthorizeService";
import Button from "react-bootstrap/Button";
import AddOrUpdateTicket from "./AddTicket/AddOrUpdateTicket";
import { DataGrid, GridApi, GridCellValue } from "@mui/x-data-grid";

export class Dashboard extends Component {
  static displayName = Dashboard.name;

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
    this.ticketStateItems = [
      { id: "1", name: "NotSentYet", title: "ارسال نشده" },
      { id: "2", name: "SentToRepair", title: "ارسال شده برای تعمیرگاه" },
      { id: "3", name: "BackFromRepair", title: "برگشت از تعمیرگاه" },
      { id: "4", name: "NeedInquiry", title: "نیاز به استعلام" },
      { id: "5", name: "Repairing", title: "در حال تعمیر" },
      { id: "6", name: "Ready", title: "آماده" },
      { id: "7", name: "Done", title: "تحویل داده شده" },
    ];
    this.columns = [
      { field: "id", headerName: "id", hide: true },
      { field: "number", headerName: "شماره", width: 170 },
      { field: "fullName", headerName: "مشتری", width: 170 },
      { field: "date", headerName: "تاریخ", width: 170 },
      { field: "customerPrice", headerName: "مبلغ", width: 170 },
      { field: "state", headerName: "وضعیت", width: 170 },
      {
        field: "action",
        headerName: "عملیات",
        width: 250,
        sortable: false,
        renderCell: (params) => {
          const api: GridApi = params.api;
          const thisRow: Record<string, GridCellValue> = {};

          api
            .getAllColumns()
            .filter((c) => c.field !== "__check__" && !!c)
            .forEach(
              (c) => (thisRow[c.field] = params.getValue(params.id, c.field))
            );

          const onEditClick = (e) => {
            e.stopPropagation();
            this.handleUpdateInvoice(thisRow);
          };
          const onDeleteClick = (e) => {
            e.stopPropagation();
            this.handleRemoveInvoice(thisRow);
          };
          return (
            <>
              <Button
                variant="warning"
                size="sm"
                style={{ marginLeft: "5px" }}
                onClick={onEditClick}
              >
                ویرایش
              </Button>
              <Button
                variant="danger"
                size="sm"
                style={{ marginLeft: "5px" }}
                onClick={onDeleteClick}
              >
                حذف
              </Button>
            </>
          );
        },
      },
    ];
    this.state = {
      invoices: [],
      loading: true,
      addModalShow: false,
    };
  }

  handleRemoveDevice = (dataItem) => {};

  handleUpdateDevice = (dataItem) => {};

  componentDidMount() {
    this.populateInvoiceData();
  }

  handleModalShow = () => {
    this.setState((prevState) => {
      return {
        addModalShow: !prevState.addModalShow,
      };
    });
  };

  render() {
    let contents = this.state.loading ? (
      <p>
        <em>بارگذاری...</em>
      </p>
    ) : (
      <div className="row">
        <Button
          variant="primary"
          className="btn-primary"
          onClick={this.handleModalShow}
        >
          جدید
        </Button>
        <div style={{ height: 300, width: "100%" }}>
          <DataGrid
            rows={this.state.invoices}
            columns={this.columns}
            pageSize={5}
            rowsPerPageOptions={[5]}
          />
        </div>
        {this.state.addModalShow && (
          <AddOrUpdateTicket
            show={this.state.addModalShow}
            onModalClose={this.handleModalShow}
          />
        )}
      </div>
    );

    return (
      <div>
        <h1 id="tabelLabel">داشبورد</h1>
        {contents}
      </div>
    );
  }

  async populateInvoiceData() {
    const token = await authService.getAccessToken();
    await fetch("api/ticket", {
      method: "get",
      headers: !token ? {} : { Authorization: `Bearer ${token}` },
    })
      .then((value) => value.json())
      .then((value) => {
        value.data.data.forEach((element) => {
          element.fullName = `${element.firstName} ${element.lastName}`;
          element.state = this.ticketStateItems.filter(
            (f) => f.id === element.state
          );
        });

        this.setState({ invoices: value.data.data, loading: false });
      });
  }
}
