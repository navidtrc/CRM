import { React, useState } from "react";
import {
  Box,
  TextField,
  Divider,
  Stack,
  Button,
  Tabs,
  Tab,
  FormControlLabel,
  IconButton,
  FormControl,
  OutlinedInput,
  InputAdornment,
  InputLabel,
  Radio,
  RadioGroup,
  FormLabel,
  Select,
  MenuItem,
} from "@mui/material/";
import Typography from "@mui/material/Typography";
import Modal from "@mui/material/Modal";
import Swal from "sweetalert2";
import PropTypes from "prop-types";
import { AdapterDateFnsJalali } from "@mui/x-date-pickers/AdapterDateFnsJalali";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { DateTimePicker } from "@mui/x-date-pickers/DateTimePicker";
import SendIcon from "@mui/icons-material/Send";
import CheckIcon from "@mui/icons-material/Check";

const style = {
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  minWidth: 700,
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

function a11yProps(index) {
  return {
    id: `simple-tab-${index}`,
    "aria-controls": `simple-tabpanel-${index}`,
  };
}

export default function AddEditTicketModal({
  open,
  onClose,
  data = {
    id: 0,
    number: "",
    lastName: "",
    username: "",
    email: "",
    phoneNumber: "",
  },
}) {
  const [value, setValue] = useState(0);
  const [age, setAge] = useState("");

  // const handleChange = (event) => {
  //   setAge(event.target.value);
  // };

  const handleChange = (event, newValue) => {
    setValue(newValue);
  };

  return (
    <div>
      <Modal
        // style={{ minWidth: "800px" }}
        open={open}
        onClose={onClose}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Box sx={style} component="form" autoComplete="off">
          <Typography id="modal-modal-title" variant="h6" component="h2">
            تیکت جدید
          </Typography>
          <Divider />

          <Box sx={{ borderBottom: 1, borderColor: "divider" }}>
            <Tabs
              value={value}
              onChange={handleChange}
              aria-label="basic tabs example"
            >
              <Tab label="تیکت" {...a11yProps(0)} />
              <Tab label="مشتری" {...a11yProps(1)} />
              <Tab label="دستگاه" {...a11yProps(2)} />
            </Tabs>
          </Box>
          <CustomTabPanel value={value} index={0}>
            <TextField
              label="شماره"
              id="ticketNumberInput"
              sx={{ m: 1, width: "25ch" }}
            />
            <LocalizationProvider dateAdapter={AdapterDateFnsJalali}>
              <DateTimePicker label="تاریخ" defaultValue={new Date()} />
            </LocalizationProvider>
          </CustomTabPanel>
          <CustomTabPanel value={value} index={1}>
            <TextField
              //value={user.firstName}
              //onChange={handleInputChange}
              name="firstName"
              required
              label="نام"
              variant="standard"
            />
            <TextField
              //  value={user.lastName}
              // onChange={handleInputChange}
              name="lastName"
              required
              label="نام خانوادگی"
              variant="standard"
            />
            <TextField
              //value={user.phoneNumber}
              //onChange={handleInputChange}
              name="phoneNumber"
              required
              label="شماره تماس"
              variant="standard"
            />
            <TextField
              //value={user.email}
              //onChange={handleInputChange}
              name="email"
              required
              label="ایمیل"
              variant="standard"
            />

            <div>
              <FormControl sx={{ m: 1, width: "25ch" }} variant="outlined">
                <InputLabel>تایید شماره همراه</InputLabel>
                <OutlinedInput
                  type="text"
                  endAdornment={
                    <InputAdornment position="end">
                      <IconButton
                        //onClick={handleClickShowPassword}
                        //onMouseDown={handleMouseDownPassword}
                        edge="end"
                      >
                        <SendIcon />
                      </IconButton>
                    </InputAdornment>
                  }
                  label="تایید شماره همراه"
                />
              </FormControl>
              <FormControl sx={{ m: 1, width: "25ch" }} variant="outlined">
                <InputLabel>تایید ایمیل</InputLabel>
                <OutlinedInput
                  type="text"
                  endAdornment={
                    <InputAdornment position="end">
                      <IconButton
                        //onClick={handleClickShowPassword}
                        //onMouseDown={handleMouseDownPassword}
                        edge="end"
                      >
                        <SendIcon />
                      </IconButton>
                    </InputAdornment>
                  }
                  label="تایید ایمیل"
                />
              </FormControl>
            </div>
          </CustomTabPanel>
          <CustomTabPanel value={value} index={2}>
            <div>
              <div>
                <FormControl variant="standard" sx={{ m: 1, minWidth: 120 }}>
                  <InputLabel>نوع دستکاه</InputLabel>
                  <Select
                    //={age}
                    //onChange={handleChange}
                    label="نوع دستکاه"
                  >
                    {/* <MenuItem value="">
            <em>None</em>
          </MenuItem> */}
                    <MenuItem value={10}>ریش تراش</MenuItem>
                    <MenuItem value={20}>اپیلیدی</MenuItem>
                    <MenuItem value={30}>سشوار</MenuItem>
                  </Select>
                </FormControl>
                <FormControl variant="standard" sx={{ m: 1, minWidth: 120 }}>
                  <InputLabel>برند دستکاه</InputLabel>
                  <Select
                    //={age}
                    //onChange={handleChange}
                    label="برند دستکاه"
                  >
                    {/* <MenuItem value="">
            <em>None</em>
          </MenuItem> */}
                    <MenuItem value={10}>Braun</MenuItem>
                    <MenuItem value={20}>Philips</MenuItem>
                    <MenuItem value={30}>Panasonic</MenuItem>
                  </Select>
                </FormControl>
                <TextField label="مدل دستگاه" variant="standard" />
              </div>
              <div>
                <TextField
                  sx={{ m: 1 }}
                  placeholder="توضیحات"
                  multiline
                  rows={4}
                  maxRows={5}
                />
                <TextField
                  sx={{ m: 1 }}
                  placeholder="متعلقات"
                  multiline
                  rows={4}
                  maxRows={5}
                />
              </div>
              <div>
                <FormControl sx={{ m: 1 }}>
                  <InputLabel>برآورد هزینه</InputLabel>
                  <OutlinedInput
                    startAdornment={
                      <InputAdornment position="start">تومان</InputAdornment>
                    }
                    label="برآورد هزینه"
                  />
                </FormControl>
                <FormControl sx={{ m: 1 }}>
                  <FormLabel id="waranty">گارانتی تعمیر</FormLabel>
                  <RadioGroup
                    name="waranty"
                    //onChange={handleChange}
                    //value={data.lockoutEnabled}
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
                </FormControl>
              </div>
              <div></div>
            </div>
          </CustomTabPanel>
          <Divider />

          <Stack mt={2} spacing={2} direction="row">
            <Button
              // onClick={() => handleSubmit()}
              variant="contained"
              color="success"
            >
              ثبت
            </Button>
            <Button onClick={() => onClose()} variant="outlined" color="error">
              انصراف
            </Button>
          </Stack>
        </Box>
      </Modal>
    </div>
  );
}
