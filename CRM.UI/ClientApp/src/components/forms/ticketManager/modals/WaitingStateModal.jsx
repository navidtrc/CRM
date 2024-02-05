import { React, useEffect, useState } from "react";
import {
  Box,
  TextField,
  Divider,
  Stack,
  Button,
  Paper,
  Autocomplete,
  InputLabel,
  Select,
  MenuItem,
  FormControl,
} from "@mui/material/";
import Typography from "@mui/material/Typography";
import Swal from "sweetalert2";
import PropTypes from "prop-types";
import TicketService from "../../../../services/ticket.service";

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

export default function WaitingStateModal() {
  const [ticket, setTicket] = useState({
    profitMargin: "",
    repairer: { id: 0, label: "" },
  });
  const [repairerSuggestions, setRepairerSuggestions] = useState(null);

  useEffect(() => {
    TicketService.getRepairers().then((result) => {
      debugger;
      setRepairerSuggestions(
        result.data.Data.map((item) => {
          return { id: item.Id, label: item.Person.Name };
        })
      );
    });
    debugger;
  }, []);

  debugger;
  return (
    <Box sx={{ mt: 2, display: "flex", justifyContent: "space-between" }}>
      <div>
        <InputLabel>حاشیه سود</InputLabel>
        <Select
          //={age}
          //onChange={handleChange}
          label="حاشیه سود"
        >
          <MenuItem value={1}>% درصد</MenuItem>
          <MenuItem value={2}>$ مبلغ</MenuItem>
        </Select>
        <TextField label="مقدار" type="number" variant="standard" />
      </div>

      <FormControl sx={{ m: 1, minWidth: 120 }}>
        <Autocomplete
          value={ticket?.repairer.label}
          onChange={(event, newValue) => {
            setTicket((prev) => ({
              ...prev,
              repairer: newValue,
            }));
          }}
          sx={{ width: 300 }}
          renderInput={(params) => <TextField {...params} label="تعمیر کار" />}
          disablePortal
          options={repairerSuggestions === null ? [] : repairerSuggestions}
        />
      </FormControl>

      <TextField
        sx={{ m: 1 }}
        value={ticket.privateDescription}
        placeholder="توضیحات (خصوصی)"
        multiline
        rows={4}
        maxRows={5}
      />
    </Box>
  );
}
