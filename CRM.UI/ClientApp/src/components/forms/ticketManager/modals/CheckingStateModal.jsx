import { React, useState } from "react";
import {
  Box,
  TextField,
  Divider,
  Stack,
  Button,
  FormControl,
  Paper,
  Radio,
  FormControlLabel,
  RadioGroup,
  FormLabel,
} from "@mui/material/";
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

export default function CheckingStateModal({ onClose, data }) {
  const [ticket, setTicket] = useState({
    ...data,
    isRepairable: false,
  });
  const [isAdmin] = useState(true);

  const handleRepairableChange = () => {
    setTicket((prev) => ({
      ...prev,
      isRepairable: !prev.isRepairable,
    }));
  };

  const handleSubmit = () => {};

  return (
    <Box sx={{ mt: 2, display: "flex", justifyContent: "space-between" }}>
      <FormControl sx={{ m: 1 }}>
        <FormLabel id="repairIsPossible">قابلیت تعمیر</FormLabel>
        <RadioGroup
          name="repairIsPossible"
          onChange={handleRepairableChange}
          value={ticket.isRepairable}
        >
          <FormControlLabel value={false} control={<Radio />} label="ندارد" />
          <FormControlLabel value={true} control={<Radio />} label="دارد" />
        </RadioGroup>
        {ticket.isRepairable && (
          <TextField
            //  value={user.lastName}
            //  onChange={handleRepairableChange}
            name="repairPrice"
            required
            label="دستمزد تعمیر"
            variant="standard"
            type="number"
          />
        )}
      </FormControl>
      <TextField
        sx={{ m: 1 }}
        placeholder="توضیحات (خصوصی)"
        multiline
        rows={4}
        maxRows={5}
      />
    </Box>
  );
}
