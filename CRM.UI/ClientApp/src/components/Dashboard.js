import React, { Component } from "react";
import authService from "./api-authorization/AuthorizeService";
import Button from "react-bootstrap/Button";
import AddOrUpdateTicket from "./AddTicket/AddOrUpdateTicket";
import InvoiceTable from "./InvoiceTable";

export class Dashboard extends Component {
  static displayName = Dashboard.name;

  constructor(props) {
    super(props);

    this.state = {
      invoices: [],
      modalInvoiceId: 0,
      loading: true,
      modalShow: false,
      paginationRequest: {
        Page: 1,
        PageSize: 50,
        Filters: null,
      },
    };
  }

  async handleRemoveInvoice(id) {
    const token = await authService.getAccessToken();
    fetch(`api/Invoice/Delete?id=${id}`, {
      method: "delete",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
        Authorization: `Bearer ${token}`,
      },
    }).then((response) => {
      if (response.isSuccess) this.loadInvoiceData();
    });
  }

  handleUpdateInvoice = (id) => {
    this.setState({
      modalShow: true,
      modalInvoiceId: id,
    });
  };

  componentDidMount() {
    this.loadInvoiceData();
  }

  handleModalShow = () => {
    this.setState((prevState) => {
      return {
        modalShow: !prevState.modalShow,
        modalInvoiceId: 0,
      };
    });
  };

  render() {
    const gridData = this.state.invoices.map((item) => {
      return {
        id: item.id,
        number: item.number,
        date: item.datePersian,
        customer: item.customer.fullName,
        price: 0,
        state: item.state.aux2,
      };
    });
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
        <div style={{ height: 300, width: "100%", marginTop: "10px" }}>
          <InvoiceTable
            items={gridData}
            onRemoveItem={this.handleRemoveInvoice}
            onUpdateItem={this.handleUpdateInvoice}
          />
        </div>
        {this.state.modalShow && (
          <AddOrUpdateTicket
            show={this.state.modalShow}
            invoiceId={this.state.modalInvoiceId}
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

  async loadInvoiceData() {
    const token = await authService.getAccessToken();
    fetch("api/Invoice/GetByPagination", {
      method: "post",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify(this.state.paginationRequest),
    })
      .then((value) => value.json())
      .then((value) => {
        this.setState({ invoices: value.data.data, loading: false });
      });
  }
}
