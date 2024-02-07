import { React, useEffect, useState } from "react";
import {
  Box,
  TextField,
  Autocomplete,
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

export default function WaitingStateModal({ actionState }) {
  const [data, setData] = actionState;

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

  return (
    <Box sx={{ mt: 2, display: "flex", justifyContent: "space-between" }}>
      <div>
        <TextField
          value={data.profit}
          onChange={(e) => {
            debugger;
            setData((prev) => {
              debugger;
              return { ...prev, profit: e.target.value };
            });
          }}
          label="قیمت فروشگاه"
          type="number"
          variant="outlined"
        />
      </div>

      <FormControl sx={{ m: 1, minWidth: 120 }}>
        <Autocomplete
          value={data.repairer.label}
          onChange={(event, newValue) => {
            setData((prev) => {
              return {
                ...prev,
                repairer: newValue,
              };
            });
          }}
          sx={{ width: 300 }}
          renderInput={(params) => <TextField {...params} label="تعمیر کار" />}
          disablePortal
          options={repairerSuggestions === null ? [] : repairerSuggestions}
        />
      </FormControl>
    </Box>
  );
}
