import { React, useEffect, useState } from "react";
import {
  Box,
  TextField,
  Divider,
  Stack,
  Button,
  fabClasses,
} from "@mui/material/";
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

export default function EmailConfirmation({ open, onClose, user }) {
  const [code, setCode] = useState(null);
  const [timer, setTimer] = useState(0);
  const [timerId, setTimerId] = useState(null);

  useEffect(() => {
    if (timer < 0) {
      clearInterval(timerId);
      setTimerId(null);
    }
  }, [timer]);

  const handleInputChange = (e) => {
    setCode(e.target.value);
  };

  const handleSendCode = () => {
    const myHeaders = new Headers();
    myHeaders.append("Content-Type", "application/json");

    var raw = JSON.stringify({
      Id: user.userId,
      Type: "0",
    });

    var requestOptions = {
      method: "POST",
      headers: myHeaders,
      body: raw,
      redirect: "follow",
    };

    fetch("/api/account/sendcode", requestOptions)
      .then((response) => response.json())
      .then((result) => {
        setTimer(5 * 60);
        const timerId = setInterval(() => {
          setTimer((prev) => prev - 1);
          console.log(1);
        }, 1000);
        setTimerId(timerId);
      })
      .catch((error) => console.log("error", error));
  };

  const handleSubmit = () => {
    const myHeaders = new Headers();
    myHeaders.append("Content-Type", "application/json");

    var raw = JSON.stringify({
      Id: user.userId,
      Type: "0",
      Code: code,
    });

    var requestOptions = {
      method: "POST",
      headers: myHeaders,
      body: raw,
      redirect: "follow",
    };

    fetch("/api/account/confirmation", requestOptions)
      .then((response) => response.json())
      .then((result) => {
        setTimer(0);
        if (result.IsSuccess) {
          onClose();
          Swal.fire({
            title: "انجام شد",
            text: "ایمیل با موفقیت تایید شد",
            icon: "success",
          });
        } else {
          onClose();
          Swal.fire({
            icon: "error",
            title: "خطا",
            text: "کد تایید اشتباه وارد شده",
          });
        }
      })
      .catch((error) => {
        console.log(error);
      });
  };

  const timerToShow = () => {
    let minute = Math.floor(timer / 60);
    let second = timer % 60;

    return `${minute}:${second}`;
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
            تایید ایمیل
          </Typography>
          <Divider />
          <Typography variant="body1" component="body1">
            کاربر: {user.username}
          </Typography>
          <Stack mt={2} spacing={2} direction="row">
            <Button
              variant="contained"
              color="info"
              disabled={timer > 0 ? true : false}
              onClick={handleSendCode}
            >
              ارسال کد تایید
            </Button>
            {timer > 0 && (
              <>
                <Typography variant="body6" component="body6">
                  {timerToShow()}
                </Typography>
                <Typography
                  variant="caption"
                  style={{ color: "green" }}
                  display="block"
                  gutterBottom
                >
                  کد تایید به ایمیل ارسال شد.
                </Typography>
              </>
            )}
          </Stack>
          <div>
            <>
              <TextField
                value={code}
                onChange={handleInputChange}
                required
                name="code"
                label="کد تایید"
                type="number"
                variant="standard"
              />
              <Button
                onClick={() => handleSubmit()}
                variant="contained"
                color="success"
                disabled={timer <= 0 ? true : false}
              >
                تایید
              </Button>
            </>
          </div>
          <Divider />

          <Stack mt={2} spacing={2} direction="row">
            <Button onClick={() => onClose()} variant="outlined" color="error">
              انصراف
            </Button>
          </Stack>
        </Box>
      </Modal>
    </div>
  );
}
