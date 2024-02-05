import { React, useState } from "react";
import { Box, TextField, Divider, Stack, Button, Paper } from "@mui/material/";
import Typography from "@mui/material/Typography";
import Swal from "sweetalert2";
import PropTypes from "prop-types";

function CustomTabPanel(props) {
  const { children, value, index, ...other } = props;

  return (
    <div
      role="tabpanel"
      hidden={value !== index}
      id={`simple-tabpanel-${index}`}
      aria-labelledby={`simple-tab-${index}`}
      {...other}
    >
      {value === index && (
        <Box sx={{ p: 3 }}>
          <Typography>{children}</Typography>
        </Box>
      )}
    </div>
  );
}

CustomTabPanel.propTypes = {
  children: PropTypes.node,
  index: PropTypes.number.isRequired,
  value: PropTypes.number.isRequired,
};

export default function ReadyToRepairStateModal({
  open,
  onClose,
  data = {
    ticketId: 0,
    ticketNumber: "123456",
    ticketDate: "2024-01-27",
    customerName: "Ali Reza",
    customerPhone: "09123456789",
    customerEmail: "ali.reza@example.com",
    phoneConfirmation: false,
    emailConfirmation: false,
    deviceType: "Laptop",
    deviceBrand: "Lenovo",
    deviceModel: "ThinkPad T14",
    descrption: "Broken screen",
    accessories: "Charger, mouse, keyboard",
    deviceWaranty: false,
    inquiryPrice: "5,000,000 تومان",
    repairerPrice: "3,000,000 تومان",
    totalPrice: "5,000,000 تومان",
  },
}) {
  const [ticketId, setTicketId] = useState(data.ticketId);
  const [isRepairable, setIsRepairable] = useState(false);
  const [isAdmin, setIsAdmin] = useState(true);

  const handleRepairableChange = () => {
    setIsRepairable((prev) => {
      return !prev;
    });
  };

  const handleSubmit = () => {};

  return (
    <Box sx={{ mt: 2, display: "flex", justifyContent: "space-between" }}>
      <TextField
        disabled
        sx={{ m: 1 }}
        placeholder="توضیحات (خصوصی)"
        multiline
        rows={4}
        maxRows={5}
      />
    </Box>
  );
}
