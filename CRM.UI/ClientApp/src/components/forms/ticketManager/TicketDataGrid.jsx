import { useMemo, useState, useEffect } from "react";
import {
  MaterialReactTable,
  useMaterialReactTable,
} from "material-react-table";
import { Button, IconButton, Tooltip } from "@mui/material";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { MRT_Localization_FA } from "material-react-table/locales/fa";
import {
  Edit as EditIcon,
  Delete as DeleteIcon,
  Refresh as RefreshIcon,
  Email as EmailIcon,
  MobileOff as MobileOffIcon,
  MobileFriendly as MobileFriendlyIcon,
  VerifiedUser as VerifiedUserIcon,
  DonutSmall as DonutSmallIcon,
  NextPlan as NextPlanIcon,
} from "@mui/icons-material/";
import Swal from "sweetalert2";

const TableGrid = ({ isRefetching, onSetIsRefetching, onOpenModal }) => {
  const [data, setData] = useState([]);
  const [isError, setIsError] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  const [rowCount, setRowCount] = useState(0);
  const [columnFilters, setColumnFilters] = useState([]);
  const [globalFilter, setGlobalFilter] = useState("");
  const [sorting, setSorting] = useState([]);
  const [validationErrors, setValidationErrors] = useState({});
  const [pagination, setPagination] = useState({
    pageIndex: 0,
    pageSize: 10,
  });

  useEffect(() => {
    const fetchData = async () => {
      if (!data.length) {
        setIsLoading(true);
      } else {
        onSetIsRefetching(true);
      }

      const fetchURL = new URL(
        "/api/ticket/get",
        process.env.NODE_ENV === "production"
          ? "https://www.material-react-table.com"
          : "https://localhost:5001"
      );

      fetchURL.searchParams.set(
        "start",
        `${pagination.pageIndex * pagination.pageSize}`
      );
      fetchURL.searchParams.set("size", `${pagination.pageSize}`);
      fetchURL.searchParams.set("filters", JSON.stringify(columnFilters ?? []));
      fetchURL.searchParams.set("globalFilter", globalFilter ?? "");
      fetchURL.searchParams.set("sorting", JSON.stringify(sorting ?? []));

      var myHeaders = new Headers();
      myHeaders.append("Content-Type", "application/json");
      var raw = JSON.stringify({
        start: Number(fetchURL.searchParams.get("start")),
        size: Number(fetchURL.searchParams.get("size")),
        globalFilter: fetchURL.searchParams.get("globalFilter"),
        filters: JSON.parse(fetchURL.searchParams.get("filters")),
        sorting: JSON.parse(fetchURL.searchParams.get("sorting"))[0],
      });
      var requestOptions = {
        method: "POST",
        headers: myHeaders,
        body: raw,
        redirect: "follow",
      };

      try {
        const response = await fetch("/api/ticket/get", requestOptions);
        const json = await response.json();
        const result = json.Data.Data.Data.map((item) => {
          const dateType = new Date(item.Date);
          const persianDate = `${dateType.toLocaleDateString(
            "fa-IR-u-nu-latn"
          )} ${dateType.toLocaleTimeString("fa-IR-u-nu-latn")}`;

          return {
            id: item.Id,
            ticketNumber: item.Number,
            ticketDate: item.Date,
            ticketPersianDate: persianDate,
            customerName: item.Customer.Person.Name,
            customerPhone: item.Customer.Person.User.PhoneNumber,
            customerEmail: item.Customer.Person.User.Email,
            StatusDisplay: item.StatusDisplay,
            status: statusHelper(item.LastStatus, item.StatusDisplay),
          };
        });

        setData(result);
        setRowCount(json.Data.Data.Total);
      } catch (error) {
        setIsError(true);
        console.error(error);
        return;
      }
      setIsError(false);
      setIsLoading(false);
      onSetIsRefetching(false);
    };
    fetchData();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [
    isRefetching,
    columnFilters,
    globalFilter,
    pagination.pageIndex,
    pagination.pageSize,
    sorting,
  ]);

  const columns = useMemo(
    () => [
      {
        accessorKey: "ticketNumber",
        header: "شماره تیکت",
        muiEditTextFieldProps: {
          type: "number",
          required: true,
          error: !!validationErrors?.ticketNumber,
          helperText: validationErrors?.ticketNumber,
          //remove any previous validation errors when user focuses on the input
          onFocus: () =>
            setValidationErrors({
              ...validationErrors,
              ticketNumber: undefined,
            }),
          //optionally add validation checking for onBlur or onChange
        },
      },
      {
        accessorKey: "ticketPersianDate",
        header: "تاریخ تیکت",
        muiEditTextFieldProps: {
          type: "text",
          required: true,
          error: !!validationErrors?.ticketPersianDate,
          helperText: validationErrors?.ticketPersianDate,
          //remove any previous validation errors when user focuses on the input
          onFocus: () =>
            setValidationErrors({
              ...validationErrors,
              ticketPersianDate: undefined,
            }),
        },
      },

      {
        accessorKey: "customerName",
        header: "مشتری",
        muiEditTextFieldProps: {
          type: "text",
          required: true,
          error: !!validationErrors?.name,
          helperText: validationErrors?.name,
          //remove any previous validation errors when user focuses on the input
          onFocus: () =>
            setValidationErrors({
              ...validationErrors,
              name: undefined,
            }),
        },
      },
      {
        accessorKey: "customerPhone",
        header: "شماره تماس",
        muiEditTextFieldProps: {
          type: "phone",
          required: true,
          error: !!validationErrors?.phoneNumber,
          helperText: validationErrors?.phoneNumber,
          //remove any previous validation errors when user focuses on the input
          onFocus: () =>
            setValidationErrors({
              ...validationErrors,
              phoneNumber: undefined,
            }),
        },
      },
      {
        accessorKey: "statusDisplay",
        header: "وضعیت",
        muiEditTextFieldProps: {
          type: "text",
          required: true,
          error: !!validationErrors?.statusDisplay,
          helperText: validationErrors?.statusDisplay,
          //remove any previous validation errors when user focuses on the input
          onFocus: () =>
            setValidationErrors({
              ...validationErrors,
              statusDisplay: undefined,
            }),
        },
      },
    ],
    [validationErrors]
  );

  const handleDeleteUser = (ticket) => {
    Swal.fire({
      title: `آیا از حذف تیکت ${ticket.number} اطمینان دارید؟`,
      text: "در صورت حذف امکان بازگشت وجود ندارد",
      icon: "warning",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "بله",
      cancelButtonText: "خیر",
    }).then((result) => {
      if (result.isConfirmed) {
        var requestOptions = {
          method: "DELETE",
          redirect: "follow",
        };

        fetch(`/api/ticket/delete?id=${ticket.id}`, requestOptions)
          .then(() => {
            Swal.fire({
              title: `تیکت ${ticket.number} حذف شد`,
              icon: "success",
            });
            onSetIsRefetching(true);
          })
          .catch((error) => console.log("error", error));
      }
    });
  };

  const table = useMaterialReactTable({
    columns,
    data,
    manualFiltering: true,
    manualPagination: true,
    manualSorting: true,
    enableFullScreenToggle: false,
    localization: MRT_Localization_FA,
    createDisplayMode: "",
    enableEditing: true,
    positionActionsColumn: "last",
    onColumnFiltersChange: setColumnFilters,
    onGlobalFilterChange: setGlobalFilter,
    onPaginationChange: setPagination,
    onSortingChange: setSorting,
    rowCount,
    state: {
      columnFilters,
      globalFilter,
      isLoading,
      pagination,
      showAlertBanner: isError,
      showProgressBars: isRefetching,
      sorting,
    },
    displayColumnDefOptions: {
      "mrt-row-actions": {
        size: 150,
        Cell: ({ row }) => (
          <>
            <Button onClick={() => onOpenModal("addedit", row.original)}>
              <EditIcon />
            </Button>

            <Button onClick={() => onOpenModal("fellow", row.original)}>
              <DonutSmallIcon />
            </Button>

            <Button onClick={() => onOpenModal("action", row.original)}>
              <NextPlanIcon />
            </Button>

            <Button
              color="error"
              onClick={() => handleDeleteUser(row.original)}
            >
              <DeleteIcon />
            </Button>
          </>
        ),
      },
    },
    renderTopToolbarCustomActions: () => (
      <>
        <Button variant="contained" onClick={() => onOpenModal("addedit")}>
          تیکت جدید
        </Button>
        <Tooltip arrow title="Refresh Data">
          <IconButton onClick={() => onSetIsRefetching(true)}>
            <RefreshIcon />
          </IconButton>
        </Tooltip>
      </>
    ),
  });

  return <MaterialReactTable table={table} />;
};

const queryClient = new QueryClient();

const TicketDataGrid = (props) => (
  <QueryClientProvider client={queryClient}>
    <TableGrid
      onOpenModal={(type, payload) => props.modalHandler(type, payload)}
      {...props}
    />
  </QueryClientProvider>
);

export default TicketDataGrid;

const statusHelper = (step, text) => {
  let buttonText = "";
  let headerText = "";

  switch (step) {
    case 0:
      buttonText = "ارسال برای بررسی تعمیر کار";
      headerText = "در صف انتظار";
      break;
    case 1:
      buttonText = "اعلام نتیجه بررسی";
      headerText = "بررسی تعمیرکار";
      break;
    case 2:
      buttonText = "ادامه";
      headerText = "بررسی مرکز";
      break;
    case 3:
      buttonText = "اعلام نتیجه استعلام";
      headerText = "استعلام";
      break;
    case 4:
      buttonText = "شروع تعمیر";
      headerText = "آماده برای تعمیر";
      break;
    case 5:
      buttonText = "اتمام تعمیر و ارسال به مرکز فروش";
      headerText = "در حال تعمیر";
      break;
    case 6:
      buttonText = "تحویل داده شد";
      headerText = "آماده ی تحویل (+)";
      break;
    case 7:
      buttonText = "تحویل داده شد";
      headerText = "آماده ی تحویل (-)";
      break;
    default:
      break;
  }

  return {
    step,
    text,
    buttonText,
    headerText,
  };
};
