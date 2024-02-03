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
import Modal from "@mui/material/Modal";
import Swal from "sweetalert2";
import PropTypes from "prop-types";
import TicketService from "../../../../services/ticket.service";

const style = {
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: "80%",
  bgcolor: "background.paper",
  border: "2px solid #000",
  boxShadow: 24,
  p: 4,
  mb: 1,
  "& .MuiTextField-root": { m: 1 },
};

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

export default function WaitingStateModal({ open, onClose, data }) {
  const [ticket, setTicket] = useState({
    ...data,
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

  const handleSubmit = () => {};

  debugger;
  return (
    <Modal
      open={open}
      onClose={onClose}
      aria-labelledby="modal-modal-title"
      aria-describedby="modal-modal-description"
    >
      <Box sx={style}>
        <Paper elevation={3} sx={{ p: 2 }}>
          <Typography id="modal-modal-title" variant="h5" component="h5">
            تیکت در صف انتظار
          </Typography>
          <Divider />
          <Box sx={{ m: 2, display: "flex", justifyContent: "space-between" }}>
            <Typography variant="h6">
              شماره تیکت: {ticket.ticketNumber}
            </Typography>
            <Typography variant="h6">
              تاریخ تیکت: {ticket.ticketPersianDate}
            </Typography>
          </Box>
          <Divider />

          <Box sx={{ mt: 2, display: "flex", justifyContent: "space-between" }}>
            <Typography variant="h6">
              نام مشتری: {ticket.customerName}
            </Typography>
            <Typography
              color={ticket.customerPhoneConfirmation ? "" : "error"}
              variant="h6"
            >
              شماره تماس: {ticket.customerPhone}
            </Typography>
            <Typography color={ticket.customerPhoneConfirmation ? "" : "error"}>
              ایمیل: {ticket.customerEmail}
            </Typography>
          </Box>
          <Divider />

          <Box sx={{ mt: 2, display: "flex", justifyContent: "space-between" }}>
            <Typography variant="h6">نوع دستگاه: {data.deviceType}</Typography>
            <Typography variant="h6">
              برند دستگاه: {ticket.deviceBrand}
            </Typography>
            <Typography ticket="h6">مدل دستگاه: {data.deviceModel}</Typography>
          </Box>
          <Box sx={{ mt: 2 }}>
            <Typography variant="h6">
              مشکل دستگاه: {data.deviceDescrption}
            </Typography>
            <Typography variant="h6">
              متعلقات دستگاه: {ticket.deviceAccessories}
            </Typography>
          </Box>
          <Box sx={{ mt: 2, display: "flex", justifyContent: "space-between" }}>
            <Typography variant="h6">
              قیمت برآورد شده: {ticket.inquiryPrice}
            </Typography>
            <Typography variant="h6">
              گارانتی :{ticket.deviceWaranty === true ? "دارد" : "ندارد"}
            </Typography>
          </Box>
          <Divider />

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
                renderInput={(params) => (
                  <TextField {...params} label="تعمیر کار" />
                )}
                disablePortal
                options={
                  repairerSuggestions === null ? [] : repairerSuggestions
                }
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

          <Stack mt={2} spacing={2} direction="row">
            <Button
              onClick={() => handleSubmit()}
              variant="contained"
              color="success"
            >
              ارسال برای بررسی تعمیر کار
            </Button>
            <Button onClick={() => onClose()} variant="contained" color="error">
              انصراف
            </Button>
          </Stack>
        </Paper>
      </Box>
    </Modal>
  );
}
const top100Films = [
  { label: "فرشید", id: 1 },
  { label: "تست 1", id: 2 },
  { label: "تست 2", id: 3 },
];
