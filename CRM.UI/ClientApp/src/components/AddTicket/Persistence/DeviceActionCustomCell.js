import * as React from "react";
import { Badge } from "react-bootstrap";
export const DeviceActionCustomCell = (props) => {
  const { dataItem } = props;
  return (
    <td className="k-command-cell">
      <button
        className="btn btn-warning btn-sm ml-1"
        onClick={() => props.onEditClick(dataItem)}
      >
        ویرایش
      </button>
      <button
        className="btn btn-danger btn-sm ml-1"
        onClick={() => props.onDeleteClick(dataItem)}
      >
        حذف
      </button>
      <button
        className="btn btn-info btn-sm ml-1"
        onClick={() => props.onInquiryClick(dataItem)}
      >
        استعلام{" "}
        {/* <Badge pill bg="danger">
          !
        </Badge> */}
      </button>
    </td>
  );
};
