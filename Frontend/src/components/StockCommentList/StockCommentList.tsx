
import { FaComments } from "react-icons/fa";
import { v4 as uuidv4 } from "uuid";
import type { CommentGet } from "../../Models/Comment";
import StockCommentListItem from "../StockCommentListItem/StockCommentListItem";

type Props = {
  comments: CommentGet[];
};

const StockCommentList = ({ comments }: Props) => {
  return (
    <div className="w-full px-4">
      {/* Heading with Icon */}
      <div className="flex items-center gap-2 mb-6 border-b pb-2">
        <FaComments className="text-lightGreen text-2xl" />
        <h2 className="text-2xl font-semibold text-gray-800">Comments</h2>
      </div>

      {comments && comments.length > 0 ? (
        comments.map((comment) => (
          <StockCommentListItem key={uuidv4()} comment={comment} />
        ))
      ) : (
        <p className="text-gray-500 italic">No comments yet.</p>
      )}
    </div>
  );
};

export default StockCommentList;