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

export default function InquiryStateModal({ onClose, data }) {
  const [ticket, setTicket] = useState({
    ...data,
    profitMargin: "",
    repairer: { id: 0, label: "" },
  });
  const [inquiryResult, setInquiryResult] = useState(false);

  const handleInquiryResultChange = () => {
    setInquiryResult((prev) => {
      return !prev;
    });
  };

  const handleSubmit = () => {};

  return (
    <Box sx={{ mt: 2, display: "flex", justifyContent: "space-between" }}>
      <FormControl sx={{ m: 1 }}>
        <FormLabel id="inquiryResult">نتیجه ی استعلام</FormLabel>
        <RadioGroup
          name="inquiryResult"
          onChange={handleInquiryResultChange}
          value={inquiryResult}
        >
          <FormControlLabel value={false} control={<Radio />} label="منفی" />
          <FormControlLabel value={true} control={<Radio />} label="مثبت" />
        </RadioGroup>
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
