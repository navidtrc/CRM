import React from "react";
import Table from "react-bootstrap/Table";
import Button from "react-bootstrap/Button";

export default class ActionTable extends React.Component {
  handleRemoveItem(id) {
    this.props.onRemoveItem(id);
  }

  handleUpdateItem(id) {
    this.props.onUpdateItem(id);
  }

  render() {
    const rows = this.props.items.map((item) => {
      return (
        <tr key={item.id}>
          <td>{item.number}</td>
          <td>{item.date}</td>
          <td>{item.customer}</td>
          <td>{item.price}</td>
          <td>{item.state}</td>
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
              onClick={this.handleRemoveItem.bind(this, item.id)}
            >
              حذف
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
              <th>شماره</th>
              <th>تاریخ</th>
              <th>مشتری</th>
              <th>مبلغ</th>
              <th>وضعیت</th>
              <th>عملیات</th>
            </tr>
          </thead>
          <tbody>{rows}</tbody>
        </Table>
      </>
    );
  }
}
