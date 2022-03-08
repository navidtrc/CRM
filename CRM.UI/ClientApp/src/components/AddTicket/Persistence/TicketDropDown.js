import React from "react";
import { Dropdown, DropdownButton } from "react-bootstrap";

export default class TicketDropDown extends React.Component {
  handleChange = (e) => {
    const selectedItem = this.props.items.filter((item) => item.id == e)[0];
    this.props.onChangeSelectedItem({
      target: {
        name: this.props.name,
        value: selectedItem,
      },
    });
  };

  render() {
    const title =
      Object.keys(this.props.selectedItem).length === 0 ||
      this.props.selectedItem === undefined
        ? "انتخاب کنید"
        : this.props.selectedItem.title;

    const dropdownList = this.props.items.map((item) => {
      return (
        <Dropdown.Item key={item.id} eventKey={item.id}>
          {item.title}
        </Dropdown.Item>
      );
    });
    return (
      <>
        <DropdownButton
          title={title}
          id="dropdown-menu-align-right"
          onSelect={this.handleChange}
        >
          {dropdownList}
        </DropdownButton>
      </>
    );
  }
}
