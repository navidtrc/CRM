import { React, useState } from "react";
import { Box, TextField, Divider, Stack, Button } from "@mui/material/";
import Typography from "@mui/material/Typography";
import Modal from "@mui/material/Modal";
import Swal from "sweetalert2";

const style = {
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  bgcolor: "background.paper",
  border: "2px solid #000",
  boxShadow: 24,
  p: 4,
  mb: 1,
  "& .MuiTextField-root": { m: 1 },
};

export default function ChangePasswordModal({ open, onClose, user }) {
  const [data, setData] = useState({ password: "", confirmPassword: "" });

  const handleInputChange = (e) => {
    setData((prev) => ({
      ...prev,
      [e.target.name]: e.target.value,
    }));
  };

  const handleSubmit = () => {
    let myHeaders = new Headers();
    myHeaders.append("Content-Type", "application/json");

    const raw = JSON.stringify({
      Id: user.userId,
      Password: data.password,
      ConfirmPassword: data.confirmPassword,
    });

    const requestOptions = {
      method: "POST",
      headers: myHeaders,
      body: raw,
      redirect: "follow",
    };

    fetch("/api/account/ForgetPasswordConfirm", requestOptions)
      .then((response) => response.json())
      .then((result) => {
        if (result.IsSuccess)
        {
          onClose();
          Swal.fire({
            title: "انجام شد",
            icon: "success",
          });
        }
        else
        {
          onClose();
          Swal.fire({
            icon: "error",
            title: "خطا",
            text: result.Message,
          });
        }
      })
      .catch((error) => {
        Swal.fire({
          icon: "error",
          title: "خطا",
          text: error.Message,
        });
        console.log("error", error);
      });
  };

  return (
    <div>
      <Modal
        open={open}
        onClose={onClose}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Box sx={style} component="form" autoComplete="off">
          <Typography id="modal-modal-title" variant="h5" component="h5">
            تغییر کلمه عبور
          </Typography>
          <Divider />
          <Typography variant="body1" component="body1">
            کاربر: {user.username}
          </Typography>

          <div>
            <>
              <TextField
                value={data.password}
                onChange={handleInputChange}
                required
                name="password"
                label="کلمه عبور"
                type="password"
                variant="standard"
              />
              <TextField
                value={data.confirmPassword}
                onChange={handleInputChange}
                required
                name="confirmPassword"
                label="تکرار کلمه عبور"
                type="password"
                variant="standard"
              />
            </>
          </div>
          <Divider />

          <Stack mt={2} spacing={2} direction="row">
            <Button
              onClick={() => handleSubmit()}
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
