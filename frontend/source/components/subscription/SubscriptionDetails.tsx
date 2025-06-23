import { Button } from "@/components/components/ui/button";

interface Subscription {
  status: string;
  plan: {
    name: string;
    interval: string;
  };
}

interface SubscriptionDetailsProps {
  subscription: Subscription;
  onCancel: () => Promise<void>;
}

export function SubscriptionDetails({
  subscription,
  onCancel,
}: SubscriptionDetailsProps) {
  return (
    <div className="bg-white dark:bg-gray-900 rounded-lg shadow-lg p-8 border border-gray-200 dark:border-gray-800">
      <div className="flex justify-between items-center mb-8">
        <div>
          <h2 className="text-2xl font-bold text-gray-900 dark:text-white">
            {subscription.plan.name}
          </h2>
          <p className="text-gray-600 dark:text-gray-400">
            Billed {subscription.plan.interval}ly
          </p>
        </div>
        <div className="text-right">
          <p className="text-sm text-gray-500 dark:text-gray-400">Status</p>
          <p className="text-lg font-semibold text-green-600 dark:text-green-400">
            {subscription.status}
          </p>
        </div>
      </div>
      <div className="mt-8">
        <Button variant="destructive" onClick={onCancel}>
          Cancel Subscription
        </Button>
      </div>
    </div>
  );
}
