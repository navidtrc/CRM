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
import Modal from "@mui/material/Modal";
import Swal from "sweetalert2";
import PropTypes from "prop-types";

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

export default function CheckingStateModal({ open, onClose, data }) {
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
    <Modal
      open={open}
      onClose={onClose}
      aria-labelledby="modal-modal-title"
      aria-describedby="modal-modal-description"
    >
      <Box sx={style}>
        <Paper elevation={3} sx={{ p: 2 }}>
          <Typography id="modal-modal-title" variant="h5" component="h5">
            جهت بررسی و قیمت گذاری تعمیرکار
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

          {isAdmin && (
            <>
              <Box
                sx={{ mt: 2, display: "flex", justifyContent: "space-between" }}
              >
                <Typography variant="h6">
                  نام مشتری: {ticket.customerName}
                </Typography>
                <Typography
                  color={ticket.customerPhoneConfirmation ? "" : "error"}
                  variant="h6"
                >
                  شماره تماس: {ticket.customerPhone}
                </Typography>
                <Typography
                  color={ticket.customerPhoneConfirmation ? "" : "error"}
                >
                  ایمیل: {ticket.customerEmail}
                </Typography>
              </Box>
              <Divider />
            </>
          )}

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

          {isAdmin && (
            <>
              <Box
                sx={{ mt: 2, display: "flex", justifyContent: "space-between" }}
              >
                <Typography variant="h6">
                  قیمت برآورد شده: {ticket.inquiryPrice}
                </Typography>
                <Typography variant="h6">
                  گارانتی :{ticket.deviceWaranty === true ? "دارد" : "ندارد"}
                </Typography>
              </Box>
            </>
          )}
          <Divider />

          <Box sx={{ mt: 2, display: "flex", justifyContent: "space-between" }}>
            <FormControl sx={{ m: 1 }}>
              <FormLabel id="repairIsPossible">قابلیت تعمیر</FormLabel>
              <RadioGroup
                name="repairIsPossible"
                onChange={handleRepairableChange}
                value={ticket.isRepairable}
              >
                <FormControlLabel
                  value={false}
                  control={<Radio />}
                  label="ندارد"
                />
                <FormControlLabel
                  value={true}
                  control={<Radio />}
                  label="دارد"
                />
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

          <Stack mt={2} spacing={2} direction="row">
            <Button
              onClick={() => handleSubmit()}
              variant="contained"
              color="success"
            >
              اعلام نتیجه بررسی به مرکز فروش
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
