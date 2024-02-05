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

export default function RepairingStateModal({ onClose, data }) {
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
