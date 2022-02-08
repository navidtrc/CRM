import React from "react";
import CurrencyInput from "react-currency-input-field";

export default class TicketPrice extends React.Component {
 
  handleChange = (e) => {
    this.props.onInputChange(e)
  };

  render() {
    return (
      <>
        <CurrencyInput
          id={this.props.inputId}
          allowDecimals={false}
          className={`form-control`}
          step={10}
          value={this.props.value}
          onValueChange={this.handleChange}
        />
      </>
    );
  }
}
