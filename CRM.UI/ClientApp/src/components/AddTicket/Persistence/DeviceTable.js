import React from "react";
import Table from "react-bootstrap/Table";
import Button from "react-bootstrap/Button";

export default class DeviceTable extends React.Component {
  handleRemoveItem(id) {
    this.props.onRemoveItem(id);
  }

  handleUpdateItem(id) {
    this.props.onUpdateItem(id);
  }

  handleInquiryItem(id) {
    this.props.onInquiryItem(id);
  }

  render() {
    const rows = this.props.items.map((item) => {
      return (
        <tr key={item.id}>
          <td>{item.type}</td>
          <td>{item.brand}</td>
          <td>{item.model}</td>
          <td>
            <Button
              variant="warning"
              className="btn-sm btn-warning"
              style={{ marginLeft: "5px" }}
              onClick={this.handleUpdateItem.bind(this, item.id)}
            >
              ویرایش
            </Button>

            <Button
              variant="danger"
              className="btn-sm btn-danger"
              style={{ marginLeft: "5px" }}
              onClick={this.handleRemoveItem.bind(this, item.id)}
            >
              حذف
            </Button>

            <Button
              variant="secondary"
              className="btn-sm btn-secondary"
              onClick={this.handleInquiryItem.bind(this, item.id)}
            >
              استعلام
            </Button>
          </td>
        </tr>
      );
    });
    return (
      <>
        <Table striped bordered hover>
          <thead>
            <tr>
              <th>نوع</th>
              <th>برند</th>
              <th>مدل</th>
              <th>عملیات</th>
            </tr>
          </thead>
          <tbody>{rows}</tbody>
        </Table>
      </>
    );
  }
}
