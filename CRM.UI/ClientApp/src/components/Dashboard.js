import React, { Component } from "react";
import authService from "./api-authorization/AuthorizeService";
import Button from "react-bootstrap/Button";
import AddOrUpdateTicket from "./AddTicket/AddOrUpdateTicket";

export class Dashboard extends Component {
  static displayName = Dashboard.name;

  constructor(props) {
    super(props);

    this.state = {
      invoices: [],
      loading: true,
      addModalShow: false,
    };
  }

  componentDidMount() {
    this.populateInvoiceData();
  }

  handleModalShow = () => {
    this.setState((prevState) => {
      return {
        addModalShow: !prevState.addModalShow
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
        <div className="col-md-12 bg-danger"></div>

        <AddOrUpdateTicket
          show={this.state.addModalShow}
          onModalClose={this.handleModalShow}
        />
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
    const response = await fetch("weatherforecast", {
      headers: !token ? {} : { Authorization: `Bearer ${token}` },
    });
    const data = await response.json();
    this.setState({ invoices: data, loading: false });
  }
}
