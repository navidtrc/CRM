import { React, useEffect, useState } from "react";
import { Box, TextField, Autocomplete, FormControl } from "@mui/material/";
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

export default function WaitingStateModal({ data, onHandleChange }) {
  const [repairerSuggestions, setRepairerSuggestions] = useState(null);
  const [selectedRepairer, setSelectedRepairer] = useState(null);

  useEffect(() => {
    TicketService.getRepairers().then((result) => {
      setRepairerSuggestions(
        result.data.Data.map((item) => {
          return { id: item.Id, label: item.Person.Name };
        })
      );
    });
  }, []);

  if (repairerSuggestions === null) {
    return "Loading";
  }

  return (
    <Box sx={{ mt: 2, display: "flex", justifyContent: "space-between" }}>
      <div>
        <TextField
          value={data.profit}
          onChange={(e) => onHandleChange(e)}
          label="قیمت فروشگاه"
          type="number"
          name="ProfitMargin"
          variant="outlined"
        />
      </div>

      <FormControl sx={{ m: 1, minWidth: 120 }}>
        <Autocomplete
          value={selectedRepairer?.label}
          onChange={(event, newValue) => {
            setSelectedRepairer(newValue);
            onHandleChange({
              target: { name: "RepairerId", value: newValue.id },
            });
          }}
          sx={{ width: 300 }}
          renderInput={(params) => <TextField {...params} label="تعمیر کار" />}
          disablePortal
          options={repairerSuggestions}
        />
      </FormControl>
    </Box>
  );
}
